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
        void addItem();
        void addWarehouse();
        void deleteItem(int id);


        void SaveChanges();
    }
}
