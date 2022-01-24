using PrzeplywDokumentowWFirmie.Logic.FactoryMethod;
using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic
{
    public class FMConector
    {
        public static void deleteItem(ItemCreator creator, int id) //delete item by id
        {
            creator.delete(id);
        }
        public static IItem findItem(ItemCreator creator, int id) //return item by id
        {
            return creator.find(id);
        }
        public static void addItem(ItemCreator creator, IItem item) //add item
        {
            creator.add(item); 
        }

        internal static void editItem(ItemCreator creator, IItem item) //edit item
        {
            creator.edit(item);
        }
    }
}