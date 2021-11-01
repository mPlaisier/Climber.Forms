using System;
using System.Collections.Generic;
using PropertyChanged;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class SubscriptionDetailViewModel : BaseViewModel<SubscriptionDetailParameter>
    {
        readonly ISubscriptionService _subscriptionService;

        Subscription _subscription;

        #region Properties

        public override string Title => "Create subscription";

        //Date
        public string DatePlaceholder => "Purchase date";

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public DateTime? SelectedDate { get; set; } = DateTime.Now;


        //Subscription Type
        public string SubscriptionTypePlaceholder => "Subscription type";

        public List<SubscriptionType> Subscriptions => SubscriptionType.GetSubscriptionTypes();
        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public SubscriptionType SelectedType { get; set; }

        //Price
        public string PricePlaceholder => "Price";

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string PriceValue { get; set; }
        public decimal Price => 0;

        //Active subscription
        public string IsActiveLabel => "Is active?";

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public bool IsActive { get; set; } = true;

        //Confirm button
        public string ConfirmButtonLabel => "Create Subscription";

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

        //TODO Add command
        Command _commandConfirm;
        public Command CommandConfirm => _commandConfirm ??= new Command(SaveSubscription);

        #endregion

        #region Constructor

        public SubscriptionDetailViewModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        #endregion

        #region LifeCycle

        public override void Init(SubscriptionDetailParameter parameter)
        {

        }

        #endregion

        #region Private

        bool IsValid()
        {
            if (SelectedDate == null)
                return false;

            if (SelectedType == null)
                return false;

            if (PriceValue == null || PriceValue == string.Empty)
                return false;

            return true;
        }

        void SaveSubscription()
        {
            var subscription = new Subscription(SelectedDate.Value, SelectedType.Type, Price, IsActive);

            _subscriptionService.AddSubscription(subscription);

            //TODO confirm with toast/message

            //TODO return to overview screen
            CoreMethods.PopPageModel(true, false, true);
        }

        #endregion
    }
}