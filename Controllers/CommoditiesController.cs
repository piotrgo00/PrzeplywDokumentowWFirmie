﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrzeplywDokumentowWFirmie.Logic.Facade;
using PrzeplywDokumentowWFirmie.Models;

namespace PrzeplywDokumentowWFirmie.Controllers
{
    public class CommoditiesController : Controller
    {
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: Commodities
        public ActionResult Index()
        {
            this.IsLoggedIn();
            return View(db.getCommodities().OrderBy(p => p.Warehouse.Name).ToList());
        }

        // GET: Commodities/Details/5
        public ActionResult Details(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.findCommodity((int)id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // GET: Commodities/Create
        public ActionResult Create(int? id)
        {
            this.IsLoggedIn();
            if (id != null)
            {
                ViewBag.OrderId = id;
                var order = db.findOrder((int)id);
                order.TransitionTo(order.StateName);
                if (!order.IsEditable())
                {
                    return HttpNotFound();
                }
            }
            ViewBag.ConsumableItemId = new SelectList(db.getConsumableItems(), "ConsumableItemId", "Name");
            ViewBag.ElectronicItemId = new SelectList(db.getElectronicItems(), "ElectronicItemId", "Name");
            ViewBag.FurnitureItemId = new SelectList(db.getFurnitureItems(), "FurnitureItemId", "Name");
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name");
            return View();
        }

        // POST: Commodities/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommodityId,Quantity,ElectronicItemId,FurnitureItemId,ConsumableItemId,WarehouseId,OrderId")] Commodity commodity)
        {
            this.IsLoggedIn();
            if (ModelState.IsValid)
            {
                db.addCommodity(commodity);
                if (commodity.OrderId == null)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", "Orders", new { id = commodity.OrderId });
            }

            ViewBag.ConsumableItemId = new SelectList(db.getConsumableItems(), "ConsumableItemId", "Name", commodity.ConsumableItemId);
            ViewBag.ElectronicItemId = new SelectList(db.getElectronicItems(), "ElectronicItemId", "Name", commodity.ElectronicItemId);
            ViewBag.FurnitureItemId = new SelectList(db.getFurnitureItems(), "FurnitureItemId", "Name", commodity.FurnitureItemId);
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", commodity.WarehouseId);
            return View(commodity);
        }

        // GET: Commodities/Edit/5
        public ActionResult Edit(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.findCommodity((int)id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", commodity.WarehouseId);
            return View(commodity);
        }

        // POST: Commodities/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommodityId,Quantity,WarehouseId")] Commodity commodity)
        {
            this.IsLoggedIn();
            if (ModelState.IsValid)
            {
                db.editCommodityPartial(commodity);
                return RedirectToAction("Index");
            }
            ViewBag.WarehouseId = new SelectList(db.getWarehouses(), "WarehouseId", "Name", commodity.WarehouseId);
            return View(commodity);
        }

        // GET: Commodities/Delete/5
        public ActionResult Delete(int? id, int? orderId)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.findCommodity((int)id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = orderId;
            return View(commodity);
        }

        // POST: Commodities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.IsLoggedIn();
            int? orderId = db.findCommodity(id).OrderId;
            db.deleteCommodity(id);
            if(orderId == null)
                return RedirectToAction("Index");
            else
                return RedirectToAction("Edit", "Orders", new { id = orderId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.dispose();
            }
            base.Dispose(disposing);
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
