using PrzeplywDokumentowWFirmie.Logic;
using PrzeplywDokumentowWFirmie.Logic.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class ElectronicItem : IItem
    {
        public int ElectronicItemId { get; set; }
        public string Name { get; set; }
        public Boolean IsUsed { get; set; }

        public IItem findItem(int id)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            return db.findElectronicItem(id);
        }

        public void delete(int id)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.deleteElectronicItem(id);
        }

        public void add(IItem item)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.addElectronicItem(item);
        }
        public void edit(IItem item)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.editElectronicItem((ElectronicItem)item);
        }
    }
    public class ConsumableItem : IItem
    {
        public int ConsumableItemId { get; set; }
        public string Name { get; set; }
        public DateTime ExpirationDate { get; set; }

        public IItem findItem(int id)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            return db.findConsumableItem(id);
        }

        public void delete(int id)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.deleteConsumableItem(id);
        }

        public void add(IItem item)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.addConsumableItem(item);
        }
        public void edit(IItem item)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.editFurnitureItem((FurnitureItem)item);
        }
    }
    public class FurnitureItem : IItem
    {
        public int FurnitureItemId { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public Boolean IsUsed { get; set; }

        public IItem findItem(int id)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            return db.findFurnitureItem(id);
        }

        public void delete(int id)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.deleteFurnitureItem(id);
        }

        public void add(IItem item)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.addFurnitureItem(item);
        }
        public void edit(IItem item)
        {
            IDatabaseConnection db = new EFDatabaseConnection();
            db.editFurnitureItem((FurnitureItem)item);
        }
    }
}