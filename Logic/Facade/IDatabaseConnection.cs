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
        List<ElectronicItem> getElectronicItems();
        List<ConsumableItem> getConsumableItems();
        List<FurnitureItem> getFurnitureItems();
        ElectronicItem findElectronicItem(int id);
        ConsumableItem findConsumableItem(int id);
        FurnitureItem findFurnitureItem(int id);
        void deleteElectronicItem(int id);
        void deleteConsumableItem(int id);
        void deleteFurnitureItem(int id);
        void addElectronicItem(IItem item);
        void addConsumableItem(IItem item);
        void addFurnitureItem(IItem item);
        void addWarehouse(); //todo


        void SaveChanges();
    }
}
