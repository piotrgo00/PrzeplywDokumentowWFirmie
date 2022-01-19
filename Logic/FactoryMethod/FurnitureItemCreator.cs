using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.FactoryMethod
{
    public class FurnitureItemCreator : ItemCreator
    {
        public override IItem CrateItem()
        {
            return new FurnitureItem();
        }
    }
}