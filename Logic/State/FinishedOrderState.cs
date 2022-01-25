using PrzeplywDokumentowWFirmie.Logic.AbstractFactory;
using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.State
{
    // finished Order - you can 
    public class FinishedOrderState : State
    {
        public override string GetState()
        {
            return "Finished Order";
        }
        public override IHtmlString GetInvoice()
        {
            InvoiceAbstractFactory factory;
            if (this._order.Firm.IsLocatedAbroad())
                factory = new ForeignInvoiceFactory();
            else
                factory = new DomesticInvoiceFactory();

            return new HtmlString(factory.getHTML(_order));
        }
        public override bool IsEditable()
        {
            return false;
        }
        public override bool IsAccepted()
        {
            return false;
        }
        public override bool IsFinished()
        {
            return true;
        }
    }
}