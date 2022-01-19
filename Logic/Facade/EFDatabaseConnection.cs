using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.Facade
{
    public class EFDatabaseConnection : IDatabaseConnection
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void addElectronicItem(IItem item)
        {
            db.ElectronicItems.Add((ElectronicItem)item);
            db.SaveChanges();
        }
        public void addConsumableItem(IItem item)
        {
            db.ConsumableItems.Add((ConsumableItem)item);
            db.SaveChanges();
        }
        public void addFurnitureItem(IItem item)
        {
            db.FurnitureItems.Add((FurnitureItem)item);
            db.SaveChanges();
        }

        public void addWarehouse()
        {
            throw new NotImplementedException();
        }

        public FurnitureItem findFurnitureItem(int id)
        {
            return db.FurnitureItems.Find(id);
        }

        public ConsumableItem findConsumableItem(int id)
        {
            return db.ConsumableItems.Find(id);
        }
        public ElectronicItem findElectronicItem(int id)
        {
            return db.ElectronicItems.Find(id);
        }

        public List<ConsumableItem> getConsumableItems()
        {
            return db.ConsumableItems.ToList();
        }

        public List<ElectronicItem> getElectronicItems()
        {
            return db.ElectronicItems.ToList();
        }

        public List<FurnitureItem> getFurnitureItems()
        {
            return db.FurnitureItems.ToList();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void deleteElectronicItem(int id)
        {
            ElectronicItem item = db.ElectronicItems.Find(id);
            db.ElectronicItems.Remove(item);
            db.SaveChanges();
        }

        public void deleteConsumableItem(int id)
        {
            ConsumableItem item = db.ConsumableItems.Find(id);
            db.ConsumableItems.Remove(item);
            db.SaveChanges();
        }

        public void deleteFurnitureItem(int id)
        {
            FurnitureItem item = db.FurnitureItems.Find(id);
            db.FurnitureItems.Remove(item);
            db.SaveChanges();
        }
    }
}