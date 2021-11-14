using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
            LoadData().ConfigureAwait(false);
        }

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is SubscriptionDetailResult result && result.IsSuccess)
            {
                if (result.Action == ECrud.Update)
                    CoreMethods.DisplayAlert(Labels.Subscription_Alert_Updated_Title, Labels.Subscription_Alert_Updated_Body, Labels.Ok);
                else if (result.Action == ECrud.Create)
                    CoreMethods.DisplayAlert(Labels.Subscription_Alert_Created_Title, Labels.Subscription_Alert_Created_Body, Labels.Ok);
                else if (result.Action == ECrud.Delete)
                    CoreMethods.DisplayAlert(Labels.Subscription_Alert_Deleted_Title, Labels.Subscription_Alert_Deleted_Body, Labels.Ok);

                Init();
            }
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            IEnumerable<Subscription> data = null;
            try
            {
                data = await _subscriptionService.GetUserSubScriptions();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }

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