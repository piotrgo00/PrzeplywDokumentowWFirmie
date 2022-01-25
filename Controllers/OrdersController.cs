﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrzeplywDokumentowWFirmie.Models;
using PrzeplywDokumentowWFirmie.Logic.State;
using PrzeplywDokumentowWFirmie.Logic.Facade;

namespace PrzeplywDokumentowWFirmie.Controllers
{
    public class OrdersController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.getOrders().ToList();

            foreach (var order in orders)
            {
                order.TransitionTo(order.StateName);
            }

            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
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
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name");
            //ViewBag.OrderId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId");
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name");
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Name,FirmId,WarehouseId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.addOrder(order);
                return RedirectToAction("Index");
            }

            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name", order.FirmId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", order.WarehouseId);
            //ViewBag.OrderId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", order.OrderId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
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
            if (!order.IsEditable())
            {
                return HttpNotFound();
            }
            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name", order.FirmId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", order.WarehouseId);
            //ViewBag.OrderId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", order.OrderId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Name,FirmId,WarehouseId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.TransitionTo(OrderState.AcceptedOrder);
                order.StateName = OrderState.AcceptedOrder;
                db.editOrder(order);
                return RedirectToAction("Index");
            }
            ViewBag.FirmId = new SelectList(db.getFirms(), "FirmId", "Name", order.FirmId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", order.WarehouseId);
            //ViewBag.OrderId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", order.OrderId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
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
            List<Commodity> commodities = db.getCommoditiesFromOrder((int)id).ToList();
            List<Commodity> notEnoughInWarehouseCommodities = new List<Commodity>();
            foreach(Commodity commodity in commodities) //find if it's possible to complete order
            {
                Commodity commodityFromWarehouse = db.findCommodityFromWarehouse(commodity);
                if (commodity.Quantity > commodityFromWarehouse.Quantity)
                {
                    notEnoughInWarehouseCommodities.Add(commodity);
                }
            }
            if(notEnoughInWarehouseCommodities.Count != 0)
            {
                return RedirectToAction("Index"); //todo
            }
            foreach (Commodity commodity in commodities) //complete possible order by reducing quantity of chosen commodities
            {
                Commodity commodityFromWarehouse = db.findCommodityFromWarehouse(commodity);
                commodityFromWarehouse.Quantity -= commodity.Quantity;
                db.editCommodityFull(commodityFromWarehouse);
            }
            order.TransitionTo(OrderState.FinishedOrder);
            db.editOrder(order);

            return RedirectToAction("Index");
        }
    }
}
