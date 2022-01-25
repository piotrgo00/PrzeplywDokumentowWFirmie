using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class Firm
    {
        public int FirmId { get; set; }
        [Display(Name = "Firm Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public ICollection<Order> Orders { get; set; }
        public bool IsLocatedAbroad()
        {
            return this.Country == "Polska" || this.Country == "Poland" ? false : true;
        }
    }
}