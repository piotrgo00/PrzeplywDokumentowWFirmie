﻿using PrzeplywDokumentowWFirmie.Models;
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

        public void addWarehouse(Warehouse warehouse)
        {
            db.Warehouses.Add(warehouse);
            this.SaveChanges();
        }

        public void addFirm(Firm firm)
        {
            db.Firms.Add(firm);
            this.SaveChanges();
        }

        public void addCommodity(Commodity commodity)
        {
            db.Commodities.Add(commodity);
            this.SaveChanges();
        }
        public void addOrder(Order order)
        {
            order.TransitionTo(order.StateName.ToState());

            db.Orders.Add(order);
            this.SaveChanges();
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

        public Commodity findCommodity(int id)
        {
            return db.Commodities.Find(id);
        }

        public Firm findFirm(int id)
        {
            return db.Firms.Find(id);
        }

        public Warehouse findWarehouse(int id)
        {
            return db.Warehouses.Find(id);
        }
        public Order findOrder(int id)
        {
            return db.Orders.Find(id);
        }
        public IQueryable<ConsumableItem> getConsumableItems()
        {
            return db.ConsumableItems;
        }

        public IQueryable<ElectronicItem> getElectronicItems()
        {
            return db.ElectronicItems;
        }

        public IQueryable<FurnitureItem> getFurnitureItems()
        {
            return db.FurnitureItems;
        }

        public IQueryable<Commodity> getCommodities()
        {
            return db.Commodities.Include(c => c.ConsumableItem).Include(c => c.ElectronicItem).Include(c => c.FurnitureItem).Include(c => c.Warehouse);
        }

        public IQueryable<Firm> getFirms()
        {
            return db.Firms;
        }

        public IQueryable<Warehouse> getWarehouses()
        {
            return db.Warehouses;
        }
        public IQueryable<Order> getOrders()
        {
            var orders = db.Orders.Include(o => o.Firm).Include(o => o.Invoice).ToList();

            foreach (var order in orders)
            {
                order.TransitionTo(order.StateName.ToState());
            }
            return db.Orders;
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

        public void deleteCommodity(int id)
        {
            Commodity commodity = db.Commodities.Find(id);
            db.Commodities.Remove(commodity);
            this.SaveChanges();
        }
        public void deleteFirm(int id)
        {
            Firm firm = db.Firms.Find(id);
            db.Firms.Remove(firm);
            this.SaveChanges();
        }
        public void deleteWarehouse(int id)
        {
            Warehouse warehouse = db.Warehouses.Find(id);
            db.Warehouses.Remove(warehouse);
            this.SaveChanges();
        }
        public void deleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

        public void editCommodity(Commodity commodity)
        {
            db.Entry(commodity).State = EntityState.Unchanged;
            db.Entry(commodity).Property(u => u.Quantity).IsModified = true;
            db.Entry(commodity).Property(u => u.WarehouseId).IsModified = true;
            this.SaveChanges();
        }
        public void editFirm(Firm firm)
        {
            db.Entry(firm).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void editWarehouse(Warehouse warehouse)
        {
            db.Entry(warehouse).State = EntityState.Modified;
            this.SaveChanges();
        }
        public void editOrder(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
            this.SaveChanges();
        }
    }
}