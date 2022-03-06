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

        public DurationSubscriptionDetail(int subscriptionId, DateTime datePurchase, ClimbingClub club, ESubscriptionType type)
            : base(subscriptionId, datePurchase, club)
        {
            if (type == ESubscriptionType.TenTurnCard || type == ESubscriptionType.SingleEntree)
                throw new ArgumentException($"{type} is not a valid subscription type for {nameof(DurationSubscriptionDetail)}");

            Type = type;

            if (Type == ESubscriptionType.OneYearSubscription)
                LblExpirationDate = $"Expires on {DatePurchase.AddYears(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
            else if (Type == ESubscriptionType.ThreeMonthSubscription)
                LblExpirationDate = $"Expires on {datePurchase.AddMonths(3).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}";
        }

        #endregion
    }
}