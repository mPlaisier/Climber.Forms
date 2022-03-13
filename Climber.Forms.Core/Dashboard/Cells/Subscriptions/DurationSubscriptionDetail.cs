using System;
using System.Globalization;

namespace Climber.Forms.Core
{
    public class DurationSubscriptionDetail : BaseSubscriptionDetail
    {
        #region Constructor

        public override ESubscriptionType Type { get; }

        public string LblExpirationDate { get; }

        #endregion

        #region Constructor

        public DurationSubscriptionDetail(Subscription subscription)
            : base(subscription)
        {
            if (subscription.Type == ESubscriptionType.TenTurnCard || subscription.Type == ESubscriptionType.SingleEntree)
                throw new ArgumentException($"{subscription.Type} is not a valid subscription type for {nameof(DurationSubscriptionDetail)}");

            Type = subscription.Type;

            if (Type == ESubscriptionType.OneYearSubscription)
                LblExpirationDate = $"Expires on {DatePurchase.AddYears(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
            else if (Type == ESubscriptionType.ThreeMonthSubscription)
                LblExpirationDate = $"Expires on {DatePurchase.AddMonths(3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
        }

        #endregion
    }
}