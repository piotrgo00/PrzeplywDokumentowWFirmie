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
            return "Confirmed";
        }
        public override Invoice GetInvoice()
        {
            // some logic returning Invoice in Order that's still waiting for accept
            return new Invoice(/* WaitingForAccept */);
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