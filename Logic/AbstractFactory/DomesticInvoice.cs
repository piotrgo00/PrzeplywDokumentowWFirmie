using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public class DomesticInvoice : IInvoice
    {
        private string companyName;
        private string companyAdress;
        private List<Commodity> items;
        private string buyerName;
        private string buyerAdress;

        public DomesticInvoice()
        {

        }

        public string CompanyName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string CompanyAdress { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public List<Commodity> Items { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string BuyerName { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public string BuyerAdress { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}