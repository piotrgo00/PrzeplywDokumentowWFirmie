using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrzeplywDokumentowWFirmie.Logic.Facade
{
    public class EFDatabaseConnection : IDatabaseConnection
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void addElectronicItem(IItem item)
        {
            db.ElectronicItems.Add((ElectronicItem)item);
            this.SaveChanges();
        }
        public void addConsumableItem(IItem item)
        {
            db.ConsumableItems.Add((ConsumableItem)item);
            this.SaveChanges();
        }
        public void addFurnitureItem(IItem item)
        {
            db.FurnitureItems.Add((FurnitureItem)item);
            this.SaveChanges();
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

        [HandleError(View = "Error")]
        public void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        throw new Exception("Must delete Commodities before Items");
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        public void deleteElectronicItem(int id)
        {
            ElectronicItem item = db.ElectronicItems.Find(id);
            db.ElectronicItems.Remove(item);
            this.SaveChanges();
        }

        public void deleteConsumableItem(int id)
        {
            ConsumableItem item = db.ConsumableItems.Find(id);
            db.ConsumableItems.Remove(item);
            this.SaveChanges();
        }

        public void deleteFurnitureItem(int id)
        {
            FurnitureItem item = db.FurnitureItems.Find(id);
            db.FurnitureItems.Remove(item);
            this.SaveChanges();
        }

        public void dispose()
        {
            db.Dispose();
        }

        public void editElectronicItem(ElectronicItem item)
        {
            db.Entry(item).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void editConsumableItem(ConsumableItem item)
        {
            db.Entry(item).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void editFurnitureItem(FurnitureItem item)
        {
            db.Entry(item).State = EntityState.Modified;
            this.SaveChanges();
        }
    }
}