using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public interface ISubscriptionService
    {
        IEnumerable<Subscription> GetSubScriptions();
        void AddSubscription(Subscription subscription);
        void UpdateSubscription(Subscription subscription);
    }
}