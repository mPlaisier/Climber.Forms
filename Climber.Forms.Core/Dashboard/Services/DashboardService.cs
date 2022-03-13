using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CBP.Extensions;
using FreshMvvm;

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

        public async Task<ObservableCollection<ICell>> GetDashboardItems(IPageModelCoreMethods pageModelCoreMethods)
        {
            var items = new RangeObservableCollection<ICell>();

            //Subscriptions
            var subscriptionCells = await GetSubscriptionItems(pageModelCoreMethods).ConfigureAwait(false);
            items.InsertRange(subscriptionCells);

            //Sessions
            var sessionCells = await GetSessionItems(pageModelCoreMethods).ConfigureAwait(false);
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
            };
        }

        async Task<RangeObservableCollection<ICell>> GetSubscriptionItems(IPageModelCoreMethods pageModelCoreMethods)
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

                cells.ForEach((cell) =>
                {
                    cell.ActionClicked = () =>
                    {
                        pageModelCoreMethods.PushPageModel<SubscriptionDetailViewModel>(cell.Subscription);
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

        async Task<RangeObservableCollection<ICell>> GetSessionItems(IPageModelCoreMethods pageModelCoreMethods)
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
                        pageModelCoreMethods.PushPageModel<ClimbingSessionDetailViewModel>(session);
                    };
                });

                details.InsertRange(cells);

                return details;
            }
            else
            {
                return CreateEmptySection(LblDashboard.Sessions_Title, LblDashboard.Sessions_Empty_Message);
            }
        }

        #endregion
    }
}