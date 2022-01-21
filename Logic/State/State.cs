using PrzeplywDokumentowWFirmie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrzeplywDokumentowWFirmie.Logic.State
{
    public abstract class State
    {
        protected Order _order;

        public void SetOrder(Order order)
        {
            this._order = order;
        }

        public abstract string GetState();
        public abstract Invoice GetInvoice();

    }
}