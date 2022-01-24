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
        IQueryable<Firm> getFirms();
        IQueryable<Warehouse> getWarehouses();
        ElectronicItem findElectronicItem(int id);
        ConsumableItem findConsumableItem(int id);
        FurnitureItem findFurnitureItem(int id);
        Commodity findCommodity(int id);
        Firm findFirm(int id);
        Warehouse findWarehouse(int id);
        void deleteElectronicItem(int id);
        void deleteConsumableItem(int id);
        void deleteFurnitureItem(int id);
        void deleteCommodity(int id);
        void deleteWarehouse(int id);
        void deleteFirm(int id);
        void addElectronicItem(IItem item);
        void addConsumableItem(IItem item);
        void addFurnitureItem(IItem item);
        void addWarehouse(Warehouse warehouse);
        void addFirm(Firm firm);
        void addCommodity(Commodity commodity);
        void editElectronicItem(ElectronicItem item);
        void editConsumableItem(ConsumableItem item);
        void editFurnitureItem(FurnitureItem item);
        void editFirm(Firm firm);
        void editWarehouse(Warehouse warehouse);
        void editCommodity(Commodity commodity);
        void SaveChanges();
        void dispose();
    }
}
