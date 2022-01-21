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
        public static void deleteItem(ItemCreator creator, int id)
        {
            creator.delete(id);
        }
        public static IItem findItem(ItemCreator creator, int id)
        {
            return creator.findItem(id);
        }
        public static void addItem(ItemCreator creator, IItem item)
        {
            creator.add(item);
        }

        internal static void editItem(ItemCreator creator, IItem item)
        {
            creator.editItem(item);
        }
    }
}