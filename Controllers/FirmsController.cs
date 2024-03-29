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
    public class FirmsController : Controller
    {
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: Firms
        public ActionResult Index()
        {
            this.IsLoggedIn();
            return View(db.getFirms());
        }

        // GET: Firms/Details/5
        public ActionResult Details(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firm firm = db.findFirm((int)id);
            ViewBag.IsLocatedAbroad = firm.IsLocatedAbroad();
            if (firm == null)
            {
                return HttpNotFound();
            }
            return View(firm);
        }

        // GET: Firms/Create
        public ActionResult Create()
        {
            this.IsLoggedIn();
            return View();
        }

        // POST: Firms/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirmId,Name,Address,Country")] Firm firm)
        {
            this.IsLoggedIn();
            if (ModelState.IsValid)
            {
                db.addFirm(firm);
                return RedirectToAction("Index");
            }

            return View(firm);
        }

        // GET: Firms/Edit/5
        public ActionResult Edit(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firm firm = db.findFirm((int)id);
            if (firm == null)
            {
                return HttpNotFound();
            }
            return View(firm);
        }

        // POST: Firms/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FirmId,Name,Address,Country")] Firm firm)
        {
            this.IsLoggedIn();
            if (ModelState.IsValid)
            {
                db.editFirm(firm);
                return RedirectToAction("Index");
            }
            return View(firm);
        }

        // GET: Firms/Delete/5
        public ActionResult Delete(int? id)
        {
            this.IsLoggedIn();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Firm firm = db.findFirm((int)id);
            if (firm == null)
            {
                return HttpNotFound();
            }
            return View(firm);
        }

        // POST: Firms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.IsLoggedIn();
            db.deleteFirm(id);
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

        private void IsLoggedIn()
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Account/Login");
            }
        }
    }
}
