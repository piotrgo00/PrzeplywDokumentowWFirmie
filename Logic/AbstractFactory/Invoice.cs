using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrzeplywDokumentowWFirmie.Models;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public class Invoice
    {
        private string companyName;
        private string companyAdress;
        private List<Commodity> items;
        private string buyerName;
        private string buyerAdress;

        public Invoice()
        {

        }

        public string CompanyName { get => companyName; set => companyName = value; }
        public string CompanyAdress { get => companyAdress; set => companyAdress = value; }
        public List<Commodity> Items { get => items; set => items = value; }
        public string BuyerName { get => buyerName; set => buyerName = value; }
        public string BuyerAdress { get => buyerAdress; set => buyerAdress = value; }
    }
}
