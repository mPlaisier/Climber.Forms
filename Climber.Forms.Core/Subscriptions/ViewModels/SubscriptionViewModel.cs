using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Climber.Forms.Core
{
    public class SubscriptionViewModel : BaseViewModel
    {
        readonly ISubscriptionService _subscriptionService;

        #region Properties

        public override string Title => Labels.Subscription_Title;

        public ObservableCollection<Subscription> Subscriptions { get; private set; }

        #endregion

        #region Commandse

        Command _commandAddSubscription;
        public Command CommandAddSubscription => _commandAddSubscription ??= new Command(async () =>
        {
            await CoreMethods.PushPageModel<SubscriptionDetailViewModel>();
        });

        #endregion

        #region Constructor

        public SubscriptionViewModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        #endregion

        #region LifeCycle

        public override void Init()
        {
            var subscriptions = new ObservableCollection<Subscription>(_subscriptionService.GetSubScriptions());

            subscriptions.ForEach((subscription) =>
            {
                subscription.ActionClicked = () =>
                {
                    CoreMethods.PushPageModel<SubscriptionDetailViewModel>(subscription);
                };
            });

            Subscriptions = subscriptions;
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is SubscriptionDetailResult result && result.IsSuccess)
            {
                if (result.IsUpdate)
                    CoreMethods.DisplayAlert(Labels.Subscription_Alert_Updated_Title, Labels.Subscription_Alert_Updated_Body, Labels.Ok);
                else
                    CoreMethods.DisplayAlert(Labels.Subscription_Alert_Created_Title, Labels.Subscription_Alert_Created_Body, Labels.Ok);

                Init();
            }
        }

        #endregion
    }
}