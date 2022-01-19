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

        public void addItem()
        {
            throw new NotImplementedException();
        }

        public void addWarehouse()
        {
            throw new NotImplementedException();
        }

        public void deleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}