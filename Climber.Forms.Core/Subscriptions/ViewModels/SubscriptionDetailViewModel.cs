using System;
using System.Collections.Generic;
using System.Linq;
using PropertyChanged;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class SubscriptionDetailViewModel : BaseViewModel<Subscription>
    {
        readonly ISubscriptionService _subscriptionService;

        Subscription _subscription;

        #region Properties

        public override string Title => Labels.SubscriptionDetail_Title;

        //Date
        public string DatePlaceholder => Labels.SubscriptionDetail_Date_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public DateTime? SelectedDate { get; set; } = DateTime.Now;

        //Subscription Type
        public string SubscriptionTypePlaceholder => Labels.SubscriptionDetail_Type_Placeholder;

        public List<SubscriptionType> Subscriptions => SubscriptionType.GetSubscriptionTypes();

        public string DefaultTypeValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public SubscriptionType SelectedType { get; set; }

        //Price
        public string PricePlaceholder => Labels.SubscriptionDetail_Price_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string PriceValue { get; set; }

        //Active subscription
        public string IsActiveLabel => Labels.SubscriptionDetail_Active_Switch_Label;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public bool IsActive { get; set; } = true;

        //Confirm button
        public string ConfirmButtonLabel => _subscription == null
                                                ? Labels.SubscriptionDetail_Button_Create_Confirm
                                                : Labels.SubscriptionDetail_Button_Update_Confirm;

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

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

        public override void Init(Subscription parameter)
        {
            _subscription = parameter;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_subscription != null)
            {
                SelectedDate = _subscription.DatePurchase;

                var type = Subscriptions.First(s => s.Type == _subscription.Type);
                DefaultTypeValue = type.Label;
                SelectedType = type;

                PriceValue = _subscription.Price.ToString();
                IsActive = _subscription.IsActive;
            }

            RaisePropertyChanged(nameof(IsConfirmButtonEnabled));
        }

        #endregion

        #region Private

        bool IsValid()
        {
            if (SelectedDate == null)
                return false;

            if (SelectedType == null)
                return false;

            if (PriceValue == null || PriceValue == string.Empty || !decimal.TryParse(PriceValue, out _))
                return false;

            return true;
        }

        void SaveSubscription()
        {
            decimal.TryParse(PriceValue, out var price);

            //Create
            if (_subscription == null)
            {
                var subscription = new Subscription(SelectedDate.Value, SelectedType.Type, price, IsActive);

                _subscriptionService.AddSubscription(subscription);
            }
            else //Update
            {
                _subscription.DatePurchase = SelectedDate.Value;
                _subscription.Type = SelectedType.Type;
                _subscription.Price = price;
                _subscription.IsActive = IsActive;

                _subscriptionService.UpdateSubscription(_subscription);
            }

            CoreMethods.PopPageModel(new SubscriptionDetailResult(true, _subscription != null), false, true);
        }

        #endregion
    }
}