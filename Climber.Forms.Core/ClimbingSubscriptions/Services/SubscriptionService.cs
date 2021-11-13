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

        /// <summary>
        /// Get all subscriptions of the user.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Subscription>> GetSubScriptions()
        {
            var dbsubscriptions = await _database.GetListAsync<DbSubscription>(x => !x.IsProtected);

            //Conver Core to Api
            var subscriptions = dbsubscriptions.Select(subscription => (Subscription)subscription)
                                               .OrderByDescending(o => o.IsActive)
                                               .ThenByDescending(t => t.DatePurchase);

            return subscriptions;
        }

        /// <summary>
        /// Gets all active subscriptions (incl. program subscriptions).
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Subscription>> GetActiveSubscriptions()
        {
            var dbSubscriptions = await _database.GetListAsync<DbSubscription>(x => x.IsActive);

            //Check that the protected subscriptions exist
            dbSubscriptions = await ValidateProtectedValues(dbSubscriptions);

            var subscriptions = dbSubscriptions.Select(subscription => (Subscription)subscription)
                                               .OrderByDescending(t => t.DatePurchase);

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

        #region Private

        async Task<List<DbSubscription>> ValidateProtectedValues(List<DbSubscription> dbSubscriptions)
        {
            if (!dbSubscriptions.Any(x => x.IsProtected))
            {
                await _database.SaveAsync((DbSubscription)ConstantsSubscriptions.ClimberSubscription);
                return await _database.GetListAsync<DbSubscription>(x => x.IsActive);
            }

            return dbSubscriptions;
        }

        #endregion
    }
}