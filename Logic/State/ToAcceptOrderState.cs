using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.State
{
    public class ToAcceptOrderState : State
    {
        public override string GetState()
        {
            return "Order waiting for accept";
        }
        public override Invoice GetInvoice()
        {
            // some logic returning Invoice in Order that's still waiting for accept
            return new Invoice(/* WaitingForAccept */);
        }
    }
}