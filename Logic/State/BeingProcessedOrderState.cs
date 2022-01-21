using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.State
{
    public class BeingProcessedOrderState : State 
    {
        public override string GetState()
        {
            return "This Order is being processed by our workers";
        }
        public override Invoice GetInvoice()
        {
            // some logic returning Invoice in Order that's still being prepared
            return new Invoice(/* ProcessedInvoice */);
        }
    }
}