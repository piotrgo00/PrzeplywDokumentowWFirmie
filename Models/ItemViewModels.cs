using PrzeplywDokumentowWFirmie.CustomDataAnnotations;
using PrzeplywDokumentowWFirmie.Logic;
using PrzeplywDokumentowWFirmie.Logic.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class ElectronicItem : IItem
    {
        public int ElectronicItemId { get; set; }
        [Required]
        [DisplayName("Item Name")]
        public string Name { get; set; }
        public Boolean IsUsed { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }

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
        [Required]
        [DisplayName("Item Name")]
        public string Name { get; set; }
        [Required]
        [DateValidator]
        [DataType(DataType.Date)]
        [DisplayName("Expiration Date")]
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
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
        [Required]
        [DisplayName("Item Name")]
        public string Name { get; set; }
        public string Condition { get; set; }
        public Boolean IsUsed { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
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