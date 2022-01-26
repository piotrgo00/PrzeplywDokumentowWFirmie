using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrzeplywDokumentowWFirmie.Logic.AbstractFactory;
using PrzeplywDokumentowWFirmie.Models;
using PrzeplywDokumentowWFirmie.Logic.State;
using PrzeplywDokumentowWFirmie.Logic.Facade;

namespace PrzeplywDokumentowWFirmie.Controllers
{
    public class OrdersController : Controller
    {
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: Orders
        public ActionResult Index()
        {
            this.IsLoggedIn();
            var orders = db.getOrders().OrderByDescending(or => or.StateName).ToList();

            // state is stored in enum StateName
            // it is required to TransitionTo order after getting date from DB to make State work correctly
            foreach (var order in orders)
            {
                order.TransitionTo(order.StateName);
            }

            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.findOrder((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }

            // after getting order from DB, it is required to set actual state
            order.TransitionTo(order.StateName);

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            this.IsLoggedIn();
            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name");
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Name,FirmId,WarehouseId")] Order order)
        {
            this.IsLoggedIn();
            if (ModelState.IsValid)
            {
                db.addOrder(order);
                return RedirectToAction("Index");
            }

            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name", order.FirmId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", order.WarehouseId);

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.findOrder((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }

            // after getting order from DB, it is required to set actual state
            order.TransitionTo(order.StateName);

            // checks if order's state allows for editing 
            // this problem may occur after manually editing url address
            if (!order.IsEditable())
            {
                return HttpNotFound();
            }

            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name", order.FirmId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", order.WarehouseId);

            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Name,FirmId,WarehouseId")] Order order)
        {
            this.IsLoggedIn();
            if (ModelState.IsValid)
            {
                // after successful editing order it becomes accepted (after clicking accept order)
                order.StateName = OrderState.AcceptedOrder;
                order.TransitionTo(order.StateName);

                db.editOrder(order);
                return RedirectToAction("Index");
            }

            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name", order.FirmId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", order.WarehouseId);

            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.findOrder((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.IsLoggedIn();
            db.deleteOrder(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RealizeOrder(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.findOrder((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.TransitionTo(order.StateName);
            if (!order.IsAccepted())
            {
                return HttpNotFound();
            }

            // get all commodities that were requested in this order
            List<Commodity> commodities = db.getCommoditiesFromOrder((int)id).ToList();

            // returning html error if finishing order isn't possible
            string notEnoughCommodities = "";

            foreach(Commodity commodity in commodities) //find if it's possible to complete order
            {
                Commodity commodityFromWarehouse = db.findCommodityFromWarehouse(commodity) != null ? db.findCommodityFromWarehouse(commodity) : new Commodity();
                if (commodity.Quantity > commodityFromWarehouse.Quantity)   // checks if there isn't enough quantity of requested items in Warehouse
                {
                    var _commodity = commodity;

                    // counting amount of missing items 
                    _commodity.Quantity = commodity.Quantity - commodityFromWarehouse.Quantity;

                    //string ItemName = _commodity.ElectronicItem != null ? _commodity.ElectronicItem.Name : _commodity.FurnitureItem != null ? _commodity.FurnitureItem.Name : _commodity.ConsumableItem != null ? _commodity.ConsumableItem.Name : null;

                    // searching for name of missing item
                    string itemName = "";
                    if (_commodity.ElectronicItem != null)
                        itemName = _commodity.ElectronicItem.Name;

                    if (_commodity.FurnitureItem != null)
                        itemName = _commodity.FurnitureItem.Name;

                    if (_commodity.ConsumableItem != null)
                        itemName = _commodity.ConsumableItem.Name;

                    string message = "<br>" + _commodity.Quantity + " pieces of " + itemName + " are missing";
                    notEnoughCommodities += message;
                }
            }

            // checks if there were any problems with finishing order
            if (notEnoughCommodities.Length != 0)
            {
                IHtmlString message = new HtmlString(order.Name + " cannot be realised: " + notEnoughCommodities);
                TempData["NotEnoughCommodities"] = message;
                return RedirectToAction("Index");
            }

            foreach (Commodity commodity in commodities) //complete possible order by reducing quantity of chosen commodities
            {
                Commodity commodityFromWarehouse = db.findCommodityFromWarehouse(commodity);
                commodityFromWarehouse.Quantity -= commodity.Quantity;
                db.editCommodityFull(commodityFromWarehouse);
            }

            // sets order as finished - allowing to get invoice
            order.StateName = OrderState.FinishedOrder;
            order.TransitionTo(order.StateName);
            db.editOrder(order);
            
            return RedirectToAction("Index");
        }

        public ActionResult Invoice(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.findOrder((int)id);
            order.TransitionTo(order.StateName);

            if (order == null)
            {
                return HttpNotFound();
            }

            IHtmlString htmlString = order.GetInvoice();

            ViewBag.htmlString = htmlString;
            ViewBag.orderId = order.OrderId;
            ViewBag.domestic = !order.Firm.IsLocatedAbroad();
            ViewBag.order = order;

            return View(order);
        }
        public FileResult DownloadInvoice(int id, bool domestic)
        {
            this.IsLoggedIn();
            Order order = db.findOrder((int)id);
            order.TransitionTo(order.StateName);

            InvoiceAbstractFactory factory;
            if (domestic)
                factory = new DomesticInvoiceFactory();
            else
                factory = new ForeignInvoiceFactory();

            byte[] bytes = PdfSharpConvert(factory.getHTML(order));
            string fileName = $"Invoice_{order.OrderId}_{order.Name}.pdf";

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //Returns a PDF file generated from given HTML code
        private byte[] PdfSharpConvert(string html)
        {
            this.IsLoggedIn();
            byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        private void IsLoggedIn()
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Account/Login");
            }
        }
    }
}
