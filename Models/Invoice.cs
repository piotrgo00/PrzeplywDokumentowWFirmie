using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public ICollection<Commodity> Commodities { get; set; }
    }
}