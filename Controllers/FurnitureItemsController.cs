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
    public class FurnitureItemsController : Controller
    {
        private IDatabaseConnection db = new EFDatabaseConnection();

        // GET: FurnitureItems
        public ActionResult Index()
        {
            return View(db.getFurnitureItems().ToList());
        }

        // GET: FurnitureItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FurnitureItem furnitureItem = (FurnitureItem)FMConector.findItem(new FurnitureItemCreator(), (int)id);
            if (furnitureItem == null)
            {
                return HttpNotFound();
            }
            return View(furnitureItem);
        }

        // GET: FurnitureItems/Create
        public ActionResult Create()
        {
            return View(new FurnitureItem());
        }

        // POST: FurnitureItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FurnitureItemId,Name,Condition,IsUsed")] FurnitureItem furnitureItem)
        {
            if (ModelState.IsValid)
            {
                FMConector.addItem(new FurnitureItemCreator(), furnitureItem);
                return RedirectToAction("Index");
            }

            return View(furnitureItem);
        }

        // GET: FurnitureItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FurnitureItem furnitureItem = (FurnitureItem)FMConector.findItem(new FurnitureItemCreator(), (int)id);
            if (furnitureItem == null)
            {
                return HttpNotFound();
            }
            return View(furnitureItem);
        }

        // POST: FurnitureItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FurnitureItemId,Name,Condition,IsUsed")] FurnitureItem furnitureItem)
        {
            if (ModelState.IsValid)
            {
                FMConector.editItem(new FurnitureItemCreator(), furnitureItem);
                return RedirectToAction("Index");
            }
            return View(furnitureItem);
        }

        // GET: FurnitureItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FurnitureItem furnitureItem = (FurnitureItem)FMConector.findItem(new FurnitureItemCreator(), (int)id);
            if (furnitureItem == null)
            {
                return HttpNotFound();
            }
            return View(furnitureItem);
        }

        // POST: FurnitureItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FMConector.deleteItem(new FurnitureItemCreator(), id);
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
