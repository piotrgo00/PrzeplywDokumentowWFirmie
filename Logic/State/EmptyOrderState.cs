using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.State
{
    public class EmptyOrderState : State 
    {
        public override string GetState()
        {
            return "Incomplete Order";
        }
        public override IHtmlString GetInvoice()
        {
            return new HtmlString("");
        }
        public override bool IsEditable()
        {
            return true;
        }
        public override bool IsAccepted()
        {
            return false;
        }
        public override bool IsFinished()
        {
            return false;
        }
    }
}