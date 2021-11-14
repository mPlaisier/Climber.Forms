using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptions();
        Task<IEnumerable<Subscription>> GetUserSubScriptions();
        Task<IEnumerable<Subscription>> GetActiveSubscriptions();

        Task AddSubscription(Subscription subscription);

        Task UpdateSubscription(Subscription subscription);

        Task DeleteSubscription(Subscription subscription);
    }
}