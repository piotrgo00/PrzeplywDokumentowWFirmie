using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrzeplywDokumentowWFirmie.Models;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public interface IInvoice
    {

        string CompanyName { get; set; }
        string CompanyAdress { get; set; }
        List<Commodity> Items { get; set; }
        string BuyerName { get; set; }
        string BuyerAdress { get; set; }
    }
}
