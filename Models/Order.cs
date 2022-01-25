using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PrzeplywDokumentowWFirmie.Logic.State;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PrzeplywDokumentowWFirmie.Models
{
    public static class Extensions
    {
        public static State ToState(this OrderState state)
        {
            switch (state)
            {
                case OrderState.EmptyOrder:
                    return new EmptyOrderState();

                case OrderState.AcceptedOrder:
                    return new AcceptedOrderState();

                case OrderState.FinishedOrder:
                    return new FinishedOrderState();

                default:
                    return new EmptyOrderState();
            }
        }
    }
    public enum OrderState
    {
        EmptyOrder,
        AcceptedOrder,
        FinishedOrder,
    }
    // context
    public class Order
    {
        public int OrderId { get; set; }
        [Display(Name = "Order name")]
        public string Name { get; set; }
        public int FirmId { get; set; }
        public virtual Firm Firm { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
        public OrderState StateName { get; set; }
        
        [NotMapped]
        private State _state = null;

        // constructor for EntityFramework
        public Order() { }
     
        public Order(OrderState stateName)
        {
            this.StateName = stateName;
            this.TransitionTo(stateName);
        }
        public void TransitionTo(OrderState stateName)
        {
            this._state = stateName.ToState();
            this.StateName = stateName;
            this._state.SetOrder(this);
        }
        public string GetState()
        {
            return this._state.GetState();
        }
        public IHtmlString GetInvoice()
        {
            return this._state.GetInvoice();
        }
        public bool IsEditable()
        {
            return this._state.IsEditable();
        }
        public bool IsAccepted() 
        {
            return this._state.IsAccepted();
        }
        public bool IsFinished()
        {
            return this._state.IsFinished();
        }

    }
}