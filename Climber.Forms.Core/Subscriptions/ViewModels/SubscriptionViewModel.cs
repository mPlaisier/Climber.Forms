using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class SubscriptionViewModel : BaseViewModel
    {
        readonly ISubscriptionService _subscriptionService;

        #region Properties

        public override string Title => "Subscriptions";

        public ObservableCollection<Subscription> Sessions { get; private set; }

        #endregion

        #region Commands

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
            Sessions = new ObservableCollection<Subscription>(_subscriptionService.GetSubScriptions());
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is bool value && value)
            {
                CoreMethods.DisplayAlert("Subscription created", "New subscription has been created!", "Ok");
                Init();
            }

        }

        #endregion
    }
}
