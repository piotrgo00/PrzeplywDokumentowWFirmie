using PrzeplywDokumentowWFirmie.Models;
using System.Collections.Generic;

namespace PrzeplywDokumentowWFirmie.Logic.AbstractFactory
{
    public class DomesticInvoiceFactory : InvoiceAbstractFactory
    {
        private readonly float VATconsumable = 0.12F,
                               VATelectronic = 0.15F,
                               VATfurniture = 0.20F;
        private float total = 0;

        public string getHTML(Order order)
        {
            string toReturn = "";

            if (order == null)
                return "NULL_ORDER";
            toReturn += HTMLhead();
            toReturn += HTMLbody(order);
            toReturn += $"<p style=\"text-align:right; margin-top: 3em;\">W sumie: {total}zł</p>";


            return toReturn;
        }



        private string HTMLhead()
        {
            string toReturn = "";

            toReturn += "<head>";
            toReturn += HTMLstyle();
            toReturn += "</head>";

            return toReturn;
        }

        private string HTMLstyle()
        {
            return "<style>" +
                   ".ClientSellerContainer {" +
                   "display: flex;" +
                   "justify-content: space-around;" +
                   "}" +
                   "table {" +
                   "width: 100%;" +
                   "}" +
                   ".inTable table, .inTable td, .inTable th {" +
                   "border: solid 1px black;" +
                   "text-align: center;" +
                   "}" +
                   "tr:nth-child(odd) {" +
                   "background-color: #f2f2f2;" +
                   "}" +
                   "</style>";
        }

        private string HTMLbody(Models.Order order)
        {
            string toReturn = "";

            if (order == null)
                return "NULL_ORDER";
            toReturn += "<h1 style=\"text-align: center;\">Faktura</h1>";
            toReturn += "<table>" +
                        "<tr>" +
                        "<td>";
            toReturn += HTMLinvoiceTo(order.Firm);
            toReturn += "</td>" +
                        "<td>";
            toReturn += HTMLinvoiceFrom();
            toReturn += "</td>" +
                        "</tr>" +
                        "</table>";
            toReturn += HTMLlistCommodities(order.Commodities);

            return toReturn;
        }

        //Returns HTML code responsible for writing out invoice recipient info
        private string HTMLinvoiceTo(Firm firm)
        {
            string toReturn = "";

            if (firm == null)
                return "NULL_FIRM";

            toReturn += "<div style=\"text-align: center;\">" +
                        "<h3>Faktura do:</h3>" +
                        $"<p>Nazwa: {firm.Name}</p>" +
                        $"<p>Adres: {firm.Address}</p>" +
                        "</div>";

            return toReturn;
        }

        //Returns HTML code responsible for writing out invoice addressee info
        private string HTMLinvoiceFrom()
        {
            string toReturn = "";

            toReturn += "<div style=\"text-align: center;\">" +
                        "<h3>Faktura od:</h3>" +
                        "<p>Nazwa: myFirmName</p>" +
                        "<p>Adres: myFirmAddress</p>" +
                        "</div>";

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
            toReturn += "<h3>Zużywalne</h3>" +
                        "<table class=\"inTable\">" +
                        "   <tr>" +
                        "       <th>Nazwa</th>" +
                        "       <th>Ilość</th>" +
                        "       <th>Data ważności</th>" +
                        "       <th>Cena NETTO</th>" +
                        "       <th>VAT</th>" +
                        "       <th>Cena BRUTTO</th>" +
                        "   </tr>";

            for (int i = 0; i < consumables.Count; i++)
            {
                toReturn += $"<tr>" +
                            $"  <td>{consumables[i].Name}</td>" +
                            $"  <td>{consumablesCount[i]}</td>" +
                            $"  <td>{consumables[i].ExpirationDate}</td>" +
                            $"  <td>{consumables[i].Price}zł</td>" +
                            $"  <td>{VATconsumable * 100}%</td>" +
                            $"  <td>{consumables[i].Price * (VATconsumable + 1)}zł</td>" +
                            $"</tr>";
                total += consumables[i].Price * (VATconsumable + 1);
            }

            toReturn += "</table>" +
                        "<h3>Elektronika</h3>" +
                        "<table class=\"inTable\">" +
                        "   <tr>" +
                        "       <th>Nazwa</th>" +
                        "       <th>Ilość</th>" +
                        "       <th>Cena NETTO</th>" +
                        "       <th>VAT</th>" +
                        "       <th>Cena BRUTTO</th>" +
                        "   </tr>";

            for (int i = 0; i < electronics.Count; i++)
            {
                toReturn += $"<tr>" +
                            $"  <td>{electronics[i].Name}</td>" +
                            $"  <td>{electronicsCount[i]}</td>" +
                            $"  <td>{electronics[i].Price}zł</td>" +
                            $"  <td>{VATelectronic * 100}%</td>" +
                            $"  <td>{electronics[i].Price * (VATelectronic + 1)}zł</td>" +
                            $"</tr>";
                total += electronics[i].Price * (VATelectronic + 1);
            }

            toReturn += "</table>" +
                        "<h3>Meble</h3>" +
                        "<table class=\"inTable\">" +
                        "   <tr>" +
                        "       <th>Nazwa</th>" +
                        "       <th>Ilość</th>" +
                        "       <th>Stan</th>" +
                        "       <th>Cena NETTO</th>" +
                        "       <th>VAT</th>" +
                        "       <th>Vena BRUTTO</th>" +
                        "   </tr>";

            for (int i = 0; i < furniture.Count; i++)
            {
                toReturn += $"<tr>" +
                            $"  <td>{furniture[i].Name}</td>" +
                            $"  <td>{furnitureCount[i]}</td>" +
                            $"  <td>{furniture[i].Condition}</td>" +
                            $"  <td>{furniture[i].Price}zł</td>" +
                            $"  <td>{VATfurniture * 100}%</td>" +
                            $"  <td>{furniture[i].Price * (VATfurniture + 1)}zł</td>" +
                            $"</tr>";
                total += furniture[i].Price * (VATfurniture + 1);
            }

            toReturn += "</table>";

            return toReturn;
        }
    }
}