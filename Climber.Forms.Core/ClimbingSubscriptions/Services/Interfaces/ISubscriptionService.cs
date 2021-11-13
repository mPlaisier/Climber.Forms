using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetSubScriptions();
        Task<IEnumerable<Subscription>> GetActiveSubscriptions();

        Task AddSubscription(Subscription subscription);

        Task UpdateSubscription(Subscription subscription);

        Task DeleteSubscription(Subscription subscription);
    }
}