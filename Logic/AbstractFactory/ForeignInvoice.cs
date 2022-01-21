using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public class ForeignInvoice : IInvoice
    {
        private string companyName;
        private string companyAdress;
        private List<Commodity> items;
        private string buyerName;
        private string buyerAdress;

        public ForeignInvoice()
        {

        }

        public string CompanyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CompanyAdress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Commodity> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BuyerName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string BuyerAdress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}