using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class Subscription
    {
        #region Properties

        public string Id { get; }

        public DateTime DatePurchase { get; set; }

        public string LblDate => DatePurchase.ToShortDateString();

        public ESubscriptionType Type { get; set; }

        public string LblType => Type.GetLabel();

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public Action ActionClicked { get; set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        public Subscription(DateTime datePurchase, ESubscriptionType type, decimal price, bool isActive = true)
        {
            Id = Guid.NewGuid().ToString();

            DatePurchase = datePurchase;
            Type = type;
            Price = price;
            IsActive = isActive;
        }

        #endregion
    }
}