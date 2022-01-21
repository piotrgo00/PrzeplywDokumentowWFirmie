using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        [DisplayName("Warehouse")]
        public string Name { get; set; }
        public ICollection<Commodity> Commodities { get; set; }
    }
}