using System.Collections.Generic;
using System.Linq;
using static Climber.Forms.Core.Enums;

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

        public IEnumerable<Subscription> GetSubScriptions()
        {
            var subscriptions = _database.Get<List<Subscription>>(EDatabaseKeys.Subscriptions);
            return subscriptions.OrderByDescending(o => o.IsActive)
                                .ThenByDescending(t => t.DatePurchase);
        }

        public void AddSubscription(Subscription subscription)
        {
            var subscriptions = GetSubScriptions().ToList();
            subscriptions.Add(subscription);

            _database.Add(subscriptions, EDatabaseKeys.Subscriptions);
        }

        #endregion
    }
}