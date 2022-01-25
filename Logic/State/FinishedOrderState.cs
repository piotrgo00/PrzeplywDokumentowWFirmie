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
        public override Invoice GetInvoice()
        {
            // Retrurning Invoice in case of finished Order
            return new Invoice();
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