using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class SubscriptionService : ISubscriptionService
    {
        readonly IDatabaseService _database;

        #region Constructor

        public SubscriptionService(IDatabaseService databaseService)
        {
            _database = databaseService;
        }

        #endregion

        #region Public

        public async Task<IEnumerable<Subscription>> GetSubScriptions()
        {
            var dbsubscriptions = await _database.GetListAsync<DbSubscription>();

            //Conver Core to Api
            var subscriptions = dbsubscriptions.Select(subscription => (Subscription)subscription)
                                               .OrderByDescending(o => o.IsActive)
                                               .ThenByDescending(t => t.DatePurchase);

            return subscriptions;
        }

        public async Task AddSubscription(Subscription subscription)
        {
            await _database.SaveAsync((DbSubscription)subscription);
        }

        public async Task UpdateSubscription(Subscription subscription)
        {
            await _database.SaveAsync((DbSubscription)subscription);
        }

        public async Task DeleteSubscription(Subscription subscription)
        {
            await _database.DeleteAsync((DbSubscription)subscription);
        }

        #endregion
    }
}