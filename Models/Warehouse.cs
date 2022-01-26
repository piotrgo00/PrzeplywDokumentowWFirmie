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
        [DisplayName("Warehouse Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}