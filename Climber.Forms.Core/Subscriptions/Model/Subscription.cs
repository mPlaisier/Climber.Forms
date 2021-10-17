using System;

namespace Climber.Forms.Core
{
    public class Subscription
    {
        #region Properties

        public DateTime DatePurchase { get; }

        public string LblDate => DatePurchase.ToShortDateString();

        public ESubscriptionType Type { get; }

        public string LblType => GetESubscriptionTypeLabel(Type);

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

        #region Private

        string GetESubscriptionTypeLabel(ESubscriptionType type)
        {
            return type switch
            {
                ESubscriptionType.TenTurnCard => "10 Turn card",
                ESubscriptionType.ThreeMonthSubscription => "3 Month subscription",
                ESubscriptionType.OneYearSubscription => "Year subscription",
                _ => throw new ArgumentException($"No label found for type {type}")
            };
        }

        #endregion
    }
}