using System;

namespace Climber.Forms.Core
{
    public abstract class BaseSubscriptionDetail : ICell
    {
        #region Properties

        public int SubscriptionId { get; }

        public DateTime DatePurchase { get; }

        public ClimbingClub Club { get; }

        public abstract ESubscriptionType Type { get; }

        public string LblType => Type.GetLabel();

        #endregion

        #region Constructor

        protected BaseSubscriptionDetail(int subscriptionId, DateTime datePurchase, ClimbingClub club)
        {
            SubscriptionId = subscriptionId;
            DatePurchase = datePurchase;
            Club = club;
        }

        #endregion
    }
}
