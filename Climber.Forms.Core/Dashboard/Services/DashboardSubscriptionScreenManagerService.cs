using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CBP.Extensions;

namespace Climber.Forms.Core
{
    /// <summary>
    /// Create the cells for subscriptions in the dashboard
    /// </summary>
    public class DashboardSubscriptionScreenManagerService : IDashboardSubscriptionScreenManagerService
    {
        readonly ClimbingTaskService _taskService;

        readonly IClimbingSessionService _sessionService;

        #region Constructor

        public DashboardSubscriptionScreenManagerService(ClimbingTaskService taskService, IClimbingSessionService sessionService)
        {
            _taskService = taskService;
            _sessionService = sessionService;
        }

        #endregion

        #region Public

        public Task<ObservableCollection<BaseSubscriptionDetail>> CreateSubscriptionCells(IEnumerable<Subscription> data)
        {
            if (data.IsNullOrEmpty())
                throw new ArgumentException($"Subscriptions should not be empty in {nameof(CreateSubscriptionCells)}");

            return ReadSubscriptionData(data);
        }

        #endregion

        #region Private

        async Task<ObservableCollection<BaseSubscriptionDetail>> ReadSubscriptionData(IEnumerable<Subscription> data)
        {
            var cells = new ObservableCollection<BaseSubscriptionDetail>();
            if (data.Any(x => x.Type == ESubscriptionType.TenTurnCard))
            {
                foreach (var tenTurnSubscription in data.Where(x => x.Type == ESubscriptionType.TenTurnCard))
                {
                    int count = 0;
                    await _taskService.Execute(async () =>
                    {
                        count = await _sessionService.GetCountForSubscription(tenTurnSubscription);
                    });

                    var quantity = new QuantitySubscriptionDetail(tenTurnSubscription,
                                                                  10 - count);
                    cells.Add(quantity);
                }
            }

            //Setup Duration subscriptions
            if (data.Any(x => x.Type == ESubscriptionType.OneYearSubscription || x.Type == ESubscriptionType.ThreeMonthSubscription))
            {
                foreach (var subscription in data.Where(x => x.Type == ESubscriptionType.OneYearSubscription
                                                          || x.Type == ESubscriptionType.ThreeMonthSubscription))
                {
                    var durationSubscription = new DurationSubscriptionDetail(subscription);
                    cells.Add(durationSubscription);
                }
            }

            return cells;
        }

        #endregion

    }
}