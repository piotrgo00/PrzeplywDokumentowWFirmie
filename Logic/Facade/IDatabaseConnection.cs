using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzeplywDokumentowWFirmie.Logic.Facade
{
    interface IDatabaseConnection
    {
        IQueryable<ElectronicItem> getElectronicItems();
        IQueryable<ConsumableItem> getConsumableItems();
        IQueryable<FurnitureItem> getFurnitureItems();
        IQueryable<Commodity> getCommodities();
        IQueryable<Commodity> getCommoditiesFromOrder(int id);
        IQueryable<Firm> getFirms();
        IQueryable<Warehouse> getWarehouses();
        IQueryable<Order> getOrders();
        ElectronicItem findElectronicItem(int id);
        ConsumableItem findConsumableItem(int id);
        FurnitureItem findFurnitureItem(int id);
        Commodity findCommodity(int id);
        Commodity findCommodityFromWarehouse(Commodity commodity);
        Firm findFirm(int id);
        Warehouse findWarehouse(int id);
        Order findOrder(int id);
        void deleteElectronicItem(int id);
        void deleteConsumableItem(int id);
        void deleteFurnitureItem(int id);
        void deleteCommodity(int id);
        void deleteWarehouse(int id);
        void deleteFirm(int id);
        void deleteOrder(int id);
        void addElectronicItem(IItem item);
        void addConsumableItem(IItem item);
        void addFurnitureItem(IItem item);
        void addWarehouse(Warehouse warehouse);
        void addFirm(Firm firm);
        void addCommodity(Commodity commodity);
        void addOrder(Order order);
        void editElectronicItem(ElectronicItem item);
        void editConsumableItem(ConsumableItem item);
        void editFurnitureItem(FurnitureItem item);
        void editFirm(Firm firm);
        void editWarehouse(Warehouse warehouse);
        void editCommodityPartial(Commodity commodity);
        void editCommodityFull(Commodity commodity);
        void editOrder(Order order);
        void SaveChanges();
        void dispose();
    }
}
