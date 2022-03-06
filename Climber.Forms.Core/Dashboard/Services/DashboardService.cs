using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CBP.Extensions;

namespace Climber.Forms.Core
{
    public class DashboardService : IDashboardService
    {
        readonly ClimbingTaskService _taskService;

        readonly ISubscriptionService _subscriptionService;
        readonly IClimbingSessionService _sessionService;

        readonly IDashboardSubscriptionScreenManagerService _subscriptionScreenManager;

        #region Constructor

        public DashboardService(ClimbingTaskService taskService,
                                IClimbingSessionService sessionService,
                                ISubscriptionService subscriptionService,
                                IDashboardSubscriptionScreenManagerService subscriptionScreenManager)
        {
            _taskService = taskService;

            _sessionService = sessionService;
            _subscriptionService = subscriptionService;

            _subscriptionScreenManager = subscriptionScreenManager;
        }

        #endregion

        #region Public

        public async Task<ObservableCollection<ICell>> GetDashboardItems()
        {
            var items = new RangeObservableCollection<ICell>();

            //Subscriptions
            var subscriptionCells = await GetSubscriptionItems();
            items.InsertRange(subscriptionCells);

            //Sessions
            var sessionCells = await GetSessionItems();
            items.InsertRange(sessionCells);

            return items;
        }

        #endregion

        #region Private

        RangeObservableCollection<ICell> CreateEmptySection(string title, string message)
        {
            return new RangeObservableCollection<ICell>
            {
                new TitleCell(title),
                new LabelCell(message),

                //TODO remove?
                //new SpaceCell()
        };
        }

        async Task<RangeObservableCollection<ICell>> GetSubscriptionItems()
        {
            //get all user active subscriptions
            IEnumerable<Subscription> data = null;
            await _taskService.Execute(async () =>
            {
                data = await _subscriptionService.GetUserSubScriptions(true);
            });

            if (data.IsNotNullAndHasItems())
            {
                var details = new RangeObservableCollection<ICell>
                {
                    new TitleCell(LblDashboard.Subscriptions_Title)
                };

                var cells = await _subscriptionScreenManager.CreateSubscriptionCells(data);
                details.InsertRange(cells);

                return details;
            }
            else
            {
                return CreateEmptySection(LblDashboard.Subscriptions_Title, LblDashboard.Subscriptions_Empty_Message);
            }
        }

        async Task<RangeObservableCollection<ICell>> GetSessionItems()
        {
            //get all user active subscriptions
            IEnumerable<ClimbingSession> data = null;
            await _taskService.Execute(async () =>
            {
                data = await _sessionService.GetClimbingSessions();
            });

            if (data.IsNotNullAndHasItems())
            {
                var details = new RangeObservableCollection<ICell>
                {
                    new TitleCell(LblDashboard.Sessions_Title)
                };

                var cells = data.Take(3);

                cells.ForEach((session) =>
                {
                    session.ActionClicked = () =>
                    {
                        //TODO
                    };
                });

                details.InsertRange(cells);

                return details;
            }
            else
            {
                return CreateEmptySection(LblDashboard.Subscriptions_Title, LblDashboard.Subscriptions_Empty_Message);
            }
        }

        #endregion
    }
}