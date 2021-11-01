using System;

namespace Climber.Forms.Core
{
    public class Subscription
    {
        #region Properties

        public DateTime DatePurchase { get; }

        public string LblDate => DatePurchase.ToShortDateString();

        public ESubscriptionType Type { get; }

        public string LblType => Type.GetLabel();

        public decimal Price { get; }

        public bool IsActive { get; }

        #endregion

        #region Constructor

        public Subscription(DateTime datePurchase, ESubscriptionType type, decimal price, bool isActive = true)
        {
            DatePurchase = datePurchase;
            Type = type;
            Price = price;
            IsActive = isActive;
        }

        #endregion
    }
}