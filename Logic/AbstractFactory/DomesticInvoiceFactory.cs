using PrzeplywDokumentowWFirmie.Models;
using System.Collections.Generic;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public class DomesticInvoiceFactory : InvoiceAbstractFactory
    {
        public string getHTML(Order order)
        {
            string toReturn = "";

            if (order == null)
                return "NULL_ORDER";

            toReturn += HTMLinvoiceTo(order.Firm);
            toReturn += HTMLinvoiceFrom();
            toReturn += HTMLlistCommodities(order.Commodities);


            return toReturn;
        }



        //Returns HTML code responsible for writing out invoice recipient info
        private string HTMLinvoiceTo(Models.Firm firm)
        {
            string toReturn = "";

            if (firm == null)
                return "NULL_FIRM";

            toReturn += "<h3>Faktura do:</h3>";
            toReturn += $"<p>Nazwa: {firm.Name}</p>";
            toReturn += $"<p>Adres: {firm.Address}</p>";

            return toReturn;
        }

        //Returns HTML code responsible for writing out invoice addressee info
        private string HTMLinvoiceFrom()
        {
            string toReturn = "";

            toReturn += "<h3>Faktura od:</h3>";
            toReturn += "<p>Nazwa: myFirmName</p>";
            toReturn += "<p>Adres: myFirmAddress</p>";

            return toReturn;
        }

        //Returns HTML code responsible for writing out info about commodieties involved within an order
        private string HTMLlistCommodities(ICollection<Models.Commodity> commodities)
        {
            string toReturn = "";

            toReturn += "<h2>Towar:</h2>";
            //Lists to sort commodities before writing out
            List<Models.ConsumableItem> consumables = new List<Models.ConsumableItem>();
            List<int> consumablesCount = new List<int>();

            List<Models.ElectronicItem> electronics = new List<Models.ElectronicItem>();
            List<int> electronicsCount = new List<int>();

            List<Models.FurnitureItem> furniture = new List<Models.FurnitureItem>();
            List<int> furnitureCount = new List<int>();

            //Filling the lists used for sorting
            foreach (var commodity in commodities)
            {
                if (commodity.ConsumableItem != null)
                {
                    consumables.Add(commodity.ConsumableItem);
                    consumablesCount.Add(commodity.Quantity);
                }
                else if (commodity.ElectronicItem != null)
                {
                    electronics.Add(commodity.ElectronicItem);
                    electronicsCount.Add(commodity.Quantity);
                }
                else if (commodity.FurnitureItem != null)
                {
                    furniture.Add(commodity.FurnitureItem);
                    furnitureCount.Add(commodity.Quantity);
                }
            }


            //Writing out the actual code
            toReturn += "<h3>Eksploatacyjne:</h3>" +
                        "<ol>";

            for (int i = 0; i < consumables.Count; i++)
            {
                toReturn += $"<li>Nazwa: {consumables[i].Name} Ilość: {consumablesCount[i]}</li>";
            }

            toReturn += "</ol>" +
                        "<h3>Elektronika</h3>" +
                        "<ol>";

            for (int i = 0; i < electronics.Count; i++)
            {
                toReturn += $"<li>Nazwa: {electronics[i].Name} Ilość: {electronicsCount[i]}</li>";
            }

            toReturn += "</ol>" +
                        "<h3>Meble</h3>" +
                        "<ol>";

            for (int i = 0; i < furniture.Count; i++)
            {
                toReturn += $"<li>Nazwa: {furniture[i].Name} Ilość: {furnitureCount[i]}</li>";
            }

            toReturn += "</ol>";

            return toReturn;
        }
    }
}