using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public class SubscriptionType
    {
        #region Properties

        public ESubscriptionType Type { get; }

        public string Label => Type.GetLabel();

        #endregion

        #region Constructor

        public SubscriptionType(ESubscriptionType type)
        {
            Type = type;
        }

        #endregion

        #region Static

        public static List<SubscriptionType> GetSubscriptionTypes()
        {
            return new List<SubscriptionType>
            {
                new SubscriptionType(ESubscriptionType.OneYearSubscription),
                new SubscriptionType(ESubscriptionType.ThreeMonthSubscription),
                new SubscriptionType(ESubscriptionType.TenTurnCard)
            };
        }

        #endregion
    }
}
