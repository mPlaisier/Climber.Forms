using System;

namespace Climber.Forms.Core
{
    public class QuantitySubscriptionDetail : BaseSubscriptionDetail
    {
        #region Constructor

        public override ESubscriptionType Type => ESubscriptionType.TenTurnCard;

        public string LblSessionsLeft { get; }

        #endregion

        #region Constructor

        public QuantitySubscriptionDetail(int subscriptionId, DateTime datePurchase, ClimbingClub club, int sessionsLeft)
            : base(subscriptionId, datePurchase, club)
        {
            LblSessionsLeft = $"{sessionsLeft} sessions left";
        }

        #endregion
    }
}