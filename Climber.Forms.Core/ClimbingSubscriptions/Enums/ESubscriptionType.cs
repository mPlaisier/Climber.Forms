using System;

namespace Climber.Forms.Core
{
    public enum ESubscriptionType
    {
        TenTurnCard,
        ThreeMonthSubscription,
        OneYearSubscription,
        SingleEntree
    }

    public static class ESubscriptionTypeExtension
    {
        public static string GetLabel(this ESubscriptionType type)
        {
            return type switch
            {
                ESubscriptionType.TenTurnCard => "10 Turn card",
                ESubscriptionType.ThreeMonthSubscription => "3 Month subscription",
                ESubscriptionType.OneYearSubscription => "Year subscription",
                ESubscriptionType.SingleEntree => "Single entree",
                _ => throw new ArgumentException($"No label found for type {type}")
            };
        }
    }
}