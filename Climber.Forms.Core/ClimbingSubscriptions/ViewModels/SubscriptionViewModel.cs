using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Climber.Forms.Core
{
    public class SubscriptionViewModel : BaseViewModel
    {
        readonly ISubscriptionService _subscriptionService;
        readonly IMessageService _messageService;
        readonly ClimbingTaskService _taskService;

        #region Properties

        public override string Title => Labels.Subscription_Title;

        public ObservableCollection<Subscription> Subscriptions { get; private set; }

        #endregion

        #region Commandse

        IAsyncCommand _commandAddSubscription;
        public IAsyncCommand CommandAddSubscription => _commandAddSubscription ??= new AsyncCommand(async () =>
        {
            await CoreMethods.PushPageModel<SubscriptionDetailViewModel>();
        });

        #endregion

        #region Constructor

        public SubscriptionViewModel(ISubscriptionService subscriptionService, IMessageService messageService, ClimbingTaskService taskService)
        {
            _subscriptionService = subscriptionService;
            _messageService = messageService;
            _taskService = taskService;
        }

        #endregion

        #region LifeCycle

        public override void Init()
        {
            LoadData().ConfigureAwait(false);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is SubscriptionDetailResult result && result.IsSuccess)
            {
                if (result.Action == ECrud.Update)
                    _messageService.ShowInfoMessage(Labels.Subscription_Alert_Updated_Body, EMessagePriority.Low);
                else if (result.Action == ECrud.Create)
                    _messageService.ShowInfoMessage(Labels.Subscription_Alert_Created_Body, EMessagePriority.Low);
                else if (result.Action == ECrud.Delete)
                    _messageService.ShowInfoMessage(Labels.Subscription_Alert_Deleted_Body, EMessagePriority.Medium);

                Init();
            }
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            IEnumerable<Subscription> data = null;

            await _taskService.Execute(async () =>
            {
                data = await _subscriptionService.GetUserSubScriptions();
            });

            if (data != null)
            {
                var subscriptions = new ObservableCollection<Subscription>(data);

                subscriptions.ForEach((subscription) =>
                {
                    subscription.ActionClicked = () =>
                    {
                        CoreMethods.PushPageModel<SubscriptionDetailViewModel>(subscription);
                    };
                });

                Subscriptions = subscriptions;
            }
        }

        #endregion
    }
}