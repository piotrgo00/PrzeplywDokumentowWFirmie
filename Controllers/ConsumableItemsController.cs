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
    public class ConsumableItemsController : Controller
    {
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: ConsumableItems
        public ActionResult Index()
        {
            return View(db.getConsumableItems().ToList());
        }

        // GET: ConsumableItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumableItem consumableItem = (ConsumableItem)FMConector.findItem(new ConsumableItemCreator(), (int)id);
            if (consumableItem == null)
            {
                return HttpNotFound();
            }
            return View(consumableItem);
        }

        // GET: ConsumableItems/Create
        public ActionResult Create()
        {
            return View(new ConsumableItem());
        }

        // POST: ConsumableItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsumableItemId,Name,ExpirationDate,Price")] ConsumableItem consumableItem)
        {
            if (ModelState.IsValid)
            {
                FMConector.addItem(new ConsumableItemCreator(), consumableItem);
                return RedirectToAction("Index");
            }

            return View(consumableItem);
        }

        // GET: ConsumableItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumableItem consumableItem = (ConsumableItem)FMConector.findItem(new ConsumableItemCreator(), (int)id);
            if (consumableItem == null)
            {
                return HttpNotFound();
            }
            return View(consumableItem);
        }

        // POST: ConsumableItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsumableItemId,Name,ExpirationDate,Price")] ConsumableItem consumableItem)
        {
            if (ModelState.IsValid)
            {
                FMConector.editItem(new ConsumableItemCreator(), consumableItem);
                return RedirectToAction("Index");
            }
            return View(consumableItem);
        }

        // GET: ConsumableItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumableItem consumableItem = (ConsumableItem)FMConector.findItem(new ConsumableItemCreator(), (int)id);
            if (consumableItem == null)
            {
                return HttpNotFound();
            }
            return View(consumableItem);
        }

        // POST: ConsumableItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FMConector.deleteItem(new ConsumableItemCreator(), id);
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
