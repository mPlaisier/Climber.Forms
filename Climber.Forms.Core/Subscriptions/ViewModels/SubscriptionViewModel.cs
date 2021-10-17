using System.Collections.ObjectModel;

namespace Climber.Forms.Core
{
    public class SubscriptionViewModel : BaseViewModel
    {
        readonly ISubscriptionService _subscriptionService;

        #region Properties

        public override string Title => "Subscriptions";

        public ObservableCollection<Subscription> Sessions { get; private set; }

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

        #endregion
    }
}
