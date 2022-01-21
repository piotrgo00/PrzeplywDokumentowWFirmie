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
            return "Finished order";
        }
        public override Invoice GetInvoice()
        {
            // Retrurning Invoice in case of finished Order
            return this._order.Invoice;
        }
    }
}