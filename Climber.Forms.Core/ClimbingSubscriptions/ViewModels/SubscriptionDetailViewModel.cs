using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyChanged;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class SubscriptionDetailViewModel : BaseViewModel<Subscription>
    {
        readonly ISubscriptionService _subscriptionService;
        readonly IClimbingClubService _clubService;
        readonly IClimbingTaskService _taskService;

        Subscription _subscription;

        #region Properties

        public override string Title => _subscription == null
                                            ? Labels.Subscription_Detail_Create_Title
                                            : Labels.Subscription_Detail_Update_Title;

        //Date
        public string DatePlaceholder => Labels.SubscriptionDetail_Date_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public DateTime? SelectedDate { get; set; } = DateTime.Now;

        //Club
        public string ClubPlaceholder => Labels.SubscriptionDetail_Club_Placeholder;

        public List<ClimbingClub> Clubs { get; private set; }
        public string DefaultClubValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public ClimbingClub SelectedClub { get; set; }

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

        IAsyncCommand _commandConfirm;
        public IAsyncCommand CommandConfirm => _commandConfirm ??= new AsyncCommand(SaveSubscription, IsValid);

        IAsyncCommand _commandDeleteSubscription;
        public IAsyncCommand CommandDeleteSubscription => _commandDeleteSubscription ??= new AsyncCommand(DeleteSubscription);

        #endregion

        #region Constructor

        public SubscriptionDetailViewModel(ISubscriptionService subscriptionService, IClimbingClubService clubService, IClimbingTaskService taskService)
        {
            _subscriptionService = subscriptionService;
            _clubService = clubService;
            _taskService = taskService;
        }

        #endregion

        #region LifeCycle

        public override void Prepare(Subscription parameter)
        {
            _subscription = parameter;
        }

        public override void Init()
        {
            LoadData().ConfigureAwait(false);
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
            return SelectedDate != null
                && SelectedClub != null
                && SelectedType != null
                && PriceValue != null && PriceValue != string.Empty && decimal.TryParse(PriceValue, out _);
        }

        async Task SaveSubscription()
        {
            decimal.TryParse(PriceValue, out var price);

            //Create
            if (_subscription == null)
            {
                var subscription = new Subscription(SelectedDate.Value, SelectedClub, SelectedType.Type, price, IsActive);

                await _taskService.Execute(async () =>
                {
                    await _subscriptionService.AddSubscription(subscription);
                });

            }
            else //Update
            {
                _subscription.DatePurchase = SelectedDate.Value;
                _subscription.Club = SelectedClub;
                _subscription.Type = SelectedType.Type;
                _subscription.Price = price;
                _subscription.IsActive = IsActive;

                await _taskService.Execute(async () =>
                {
                    await _subscriptionService.UpdateSubscription(_subscription);
                });
            }

            await CoreMethods.PopPageModel(new SubscriptionDetailResult(true, _subscription != null ? ECrud.Update : ECrud.Create), false, true);
        }

        async Task DeleteSubscription()
        {
            if (_subscription != null)
            {
                await _taskService.ExecuteDelete(async () =>
                {
                    await _subscriptionService.DeleteSubscription(_subscription);
                    await CoreMethods.PopPageModel(new SubscriptionDetailResult(true, ECrud.Delete), false, true);
                });
            }
        }

        async Task LoadData()
        {
            IEnumerable<ClimbingClub> data = null;

            await _taskService.Execute(async () =>
            {
                data = await _clubService.GetClubs();
            });

            if (data != null)
            {
                Clubs = new List<ClimbingClub>(data);

                if (_subscription?.Club != null)
                {
                    var club = Clubs.First(s => s.Id == _subscription.Club.Id);
                    DefaultClubValue = club.Name;
                    SelectedClub = club;
                }
            }
        }

        #endregion
    }
}