//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using PrzeplywDokumentowWFirmie.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class Commodity
    {
        /*IItem commodity;
        int quantity;

        public IItem Item { get => commodity; set => commodity = value; }
        public int Quantity { get => quantity; set => quantity = value; }*/
        public int CommodityId { get; set; }
        public int Quantity { get; set; }
        public int? ElectronicItemId { get; set; }
        public virtual ElectronicItem ElectronicItem { get; set; }
        public int? FurnitureItemId { get; set; }
        public virtual FurnitureItem FurnitureItem { get; set; }
        public int? ConsumableItemId { get; set; }
        public virtual ConsumableItem ConsumableItem { get; set; }

    }
}