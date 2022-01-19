using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.FactoryMethod
{
    public class ConsumableItemCreator : ItemCreator
    {
        public override IItem CrateItem()
        {
            return new ConsumableItem();
        }
    }
}