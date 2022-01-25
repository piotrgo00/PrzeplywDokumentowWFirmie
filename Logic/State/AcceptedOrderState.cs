using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.State
{
    public class AcceptedOrderState : State
    {
        public override string GetState()
        {
            return "Completed Order";
        }
        public override IHtmlString GetInvoice()
        {
            return new HtmlString("");
        }
        public override bool IsEditable()
        {
            return false;
        }
        public override bool IsAccepted()
        {
            return true;
        }
        public override bool IsFinished()
        {
            return false;
        }
    }
}