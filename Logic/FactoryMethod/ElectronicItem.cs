using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.FactoryMethod
{
    public class ElectronicItem : IItem
    {
        public void Create()
        {
            throw new NotImplementedException();
        }

        public IItem getItemById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}