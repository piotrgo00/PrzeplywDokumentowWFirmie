﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.FactoryMethod
{
    public class ElectronicItemCreator : ItemCreator
    {
        public override IItem CrateItem()
        {
            return new ElectronicItem();
        }
    }
}