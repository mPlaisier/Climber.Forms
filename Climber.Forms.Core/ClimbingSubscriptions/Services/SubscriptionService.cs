using System;
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

        public async Task<IEnumerable<Subscription>> GetAllSubscriptions()
        {
            var dbsubscriptions = await _database.GetListAsync<DbSubscription>();

            var subscriptions = await CreateSubscriptionsFromResult(dbsubscriptions).ConfigureAwait(false);

            return subscriptions.OrderByDescending(o => o.IsActive)
                                .ThenByDescending(t => t.DatePurchase);
        }

        /// <summary>
        /// Get all subscriptions of the user.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Subscription>> GetUserSubScriptions()
        {
            var dbSubscriptions = await _database.GetListAsync<DbSubscription>();

            var subscriptions = await CreateSubscriptionsFromResult(dbSubscriptions.Where(x => !x.IsProtected).ToList()).ConfigureAwait(false);

            return subscriptions.OrderByDescending(o => o.IsActive)
                                .ThenByDescending(t => t.DatePurchase);
        }

        /// <summary>
        /// Gets all active subscriptions (incl. program subscriptions).
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Subscription>> GetActiveSubscriptions()
        {
            var dbSubscriptions = await _database.GetListAsync<DbSubscription>(x => x.IsActive);

            //Check that the protected subscriptions exist
            dbSubscriptions = await ValidateProtectedValues(dbSubscriptions).ConfigureAwait(false);

            var subscriptions = await CreateSubscriptionsFromResult(dbSubscriptions).ConfigureAwait(false);

            return subscriptions.OrderByDescending(t => t.DatePurchase);
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

        async Task<List<Subscription>> CreateSubscriptionsFromResult(List<DbSubscription> dbSubscriptions)
        {
            var subscriptions = new List<Subscription>();

            foreach (var dbSubscription in dbSubscriptions)
            {
                DbClimbingClub dbClub = null;
                if (dbSubscription.ClubId.HasValue)
                    dbClub = await _database.GetAsync<DbClimbingClub>(dbSubscription.ClubId.Value);

                subscriptions.Add(new Subscription(dbSubscription, (ClimbingClub)dbClub));
            }
            return subscriptions;
        }

        #endregion
    }
}