namespace Climber.Forms.Core
{
    public class QuantitySubscriptionDetail : BaseSubscriptionDetail
    {
        #region Constructor

        public override ESubscriptionType Type => ESubscriptionType.TenTurnCard;

        public string LblSessionsLeft { get; }

        #endregion

        #region Constructor

        public QuantitySubscriptionDetail(Subscription subscription, int sessionsLeft)
            : base(subscription)
        {
            LblSessionsLeft = $"{sessionsLeft} sessions left";
        }

        #endregion
    }
}