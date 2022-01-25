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

namespace PrzeplywDokumentowWFirmie.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Firm).Include(o => o.Invoice).ToList();

            foreach (var order in orders)
            {
                order.TransitionTo(order.StateName.ToState());
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
            Order order = db.Orders.Find(id);
            order.TransitionTo(order.StateName.ToState());
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Name");
            //ViewBag.OrderId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId");
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,Name,FirmId,InvoiceId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Name", order.FirmId);
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
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            order.TransitionTo(order.StateName.ToState());
            if (!order.IsEditable())
            {
                return HttpNotFound();
            }
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Name", order.FirmId);
            //ViewBag.OrderId = new SelectList(db.Invoices, "InvoiceId", "InvoiceId", order.OrderId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Name,FirmId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.TransitionTo(new AcceptedOrderState());
                order.StateName = OrderState.AcceptedOrder;

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirmId = new SelectList(db.Firms, "FirmId", "Name", order.FirmId);
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
            Order order = db.Orders.Find(id);
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
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
