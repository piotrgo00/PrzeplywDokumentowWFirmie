using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PrzeplywDokumentowWFirmie.Logic.State;

namespace PrzeplywDokumentowWFirmie.Models
{
    // context
    public class Order
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int InvoiceId { get; set; }

        public virtual Firm Buyer { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
        
        [NotMapped]
        private State _state = null;

        public Order(State state)
        {
            this.TransitionTo(state);
        }
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