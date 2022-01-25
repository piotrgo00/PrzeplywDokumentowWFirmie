using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrzeplywDokumentowWFirmie.Logic;
using PrzeplywDokumentowWFirmie.Logic.Facade;
using PrzeplywDokumentowWFirmie.Logic.FactoryMethod;
using PrzeplywDokumentowWFirmie.Models;

namespace PrzeplywDokumentowWFirmie.Controllers
{
    public class ElectronicItemsController : Controller
    {
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: ElectronicItems
        public ActionResult Index()
        {
            return View(db.getElectronicItems().ToList());
        }

        // GET: ElectronicItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectronicItem electronicItem = (ElectronicItem)FMConector.findItem(new ElectronicItemCreator(), (int)id);
            if (electronicItem == null)
            {
                return HttpNotFound();
            }
            return View(electronicItem);
        }

        // GET: ElectronicItems/Create
        public ActionResult Create()
        {
            return View(new ElectronicItem());
        }

        // POST: ElectronicItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ElectronicItemId,Name,IsUsed,Price")] ElectronicItem electronicItem)
        {
            if (ModelState.IsValid)
            {
                FMConector.addItem(new ElectronicItemCreator(), electronicItem);
                return RedirectToAction("Index");
            }

            return View(electronicItem);
        }

        // GET: ElectronicItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectronicItem electronicItem = (ElectronicItem)FMConector.findItem(new ElectronicItemCreator(), (int)id);
            if (electronicItem == null)
            {
                return HttpNotFound();
            }
            return View(electronicItem);
        }

        // POST: ElectronicItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ElectronicItemId,Name,IsUsed,Price")] ElectronicItem electronicItem)
        {
            if (ModelState.IsValid)
            {
                FMConector.editItem(new ElectronicItemCreator(), electronicItem);
                return RedirectToAction("Index");
            }
            return View(electronicItem);
        }

        // GET: ElectronicItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ElectronicItem electronicItem = (ElectronicItem)FMConector.findItem(new ElectronicItemCreator(), (int)id);
            if (electronicItem == null)
            {
                return HttpNotFound();
            }
            return View(electronicItem);
        }

        // POST: ElectronicItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FMConector.deleteItem(new ElectronicItemCreator(), id);
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
    }
}
