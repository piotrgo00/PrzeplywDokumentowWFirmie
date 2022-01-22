using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrzeplywDokumentowWFirmie.Logic.State;

namespace PrzeplywDokumentowWFirmie.Models
{
    // context
    public class Order
    {
        public int OrderId { get; set; }
        public Firm Buyer { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
        public Invoice Invoice { get; set; }

        private State _state = null;

        public void TransitionTo(State state)
        {
            this._state = state;
            this._state.SetOrder(this);
        }

        public Invoice GetInvoice()
        {
            return this._state.GetInvoice();
        }
    }
}