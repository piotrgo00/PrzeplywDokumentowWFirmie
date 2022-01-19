using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.FactoryMethod
{
    public abstract class ItemCreator
    {
        public abstract IItem CreateItem();

        public void delete(int id)
        {
            var item = CreateItem();
            item.delete(id);
        }
        public void add(IItem itemToAdd)
        {
            var item = CreateItem();
            item.add(itemToAdd);
        }
        public IItem findItem(int id)
        {
            var item = CreateItem();
            return item.findItem(id);
        }

    }
}