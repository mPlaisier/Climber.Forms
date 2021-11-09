using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class ClimbingSessionDetailViewModel : BaseViewModel<ClimbingSession>
    {
        readonly IClimbingSessionService _climbingSessionService;
        readonly ISubscriptionService _subscriptionService;

        ClimbingSession _session;

        #region Properties

        public override string Title => _session == null
                                            ? Labels.Session_Detail_Create_Title
                                            : Labels.Session_Detail_Update_Title;

        //Date
        public string DatePlaceholder => Labels.Session_Detail_Date_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public DateTime? SelectedDate { get; set; } = DateTime.Now;

        //Subscription
        public string SubscriptionPlaceholder => Labels.Session_Detail_Subscription_Placeholder;

        public List<Subscription> Subscriptions { get; set; }
        public string DefaultSubscriptionValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public Subscription SelectedSubscription { get; set; }

        //Climbing Type
        public string ClimbingTypePlaceholder => Labels.Session_Detail_Climbing_Type_Placeholder;

        public List<ClimbingType> ClimbingTypes => ClimbingType.GetClimbingTypes();

        public string DefaultClimbingTypeValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public ClimbingType SelectedClimbingType { get; set; }

        //Confirm button
        public string ConfirmButtonLabel => _session == null
                                                ? Labels.Session_Detail_Button_Create_Confirm
                                                : Labels.Session_Detail_Button_Update_Confirm;

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

        ICommand _commandConfirm;
        public ICommand CommandConfirm => _commandConfirm ??= new Command(async () => await SaveSession().ConfigureAwait(false));

        #endregion

        #region Constructor

        public ClimbingSessionDetailViewModel(IClimbingSessionService climbingSessionService, ISubscriptionService subscriptionService)
        {
            _climbingSessionService = climbingSessionService;
            _subscriptionService = subscriptionService;
        }

        #endregion

        #region LifeCycle

        public override void Prepare(ClimbingSession parameter)
        {
            _session = parameter;
        }

        public override void Init()
        {
            LoadData().ConfigureAwait(false);
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_session != null)
            {
                SelectedDate = _session.Date;

                //Climbing type
                var type = ClimbingTypes.First(s => s.Type == _session.Type);
                DefaultClimbingTypeValue = type.Label;
                SelectedClimbingType = type;
            }

            RaisePropertyChanged(nameof(IsConfirmButtonEnabled));
        }

        #endregion

        #region Private

        bool IsValid()
        {
            if (SelectedDate == null || !SelectedDate.HasValue)
                return false;

            if (SelectedSubscription == null)
                return false;

            if (SelectedClimbingType == null)
                return false;

            return true;
        }

        async Task SaveSession()
        {
            //Create
            if (_session == null)
            {
                var session = new ClimbingSession(SelectedDate.Value, SelectedSubscription, SelectedClimbingType);

                try
                {
                    await _climbingSessionService.SaveSession(session);
                    await CoreMethods.PopPageModel(new SessionDetailResult(true, ECrud.Create), false, true);
                }
                catch (Exception ex)
                {
                    await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
                }
            }
            else //Update
            {
                _session.Date = SelectedDate.Value;
                _session.Subscription = SelectedSubscription;
                _session.Type = SelectedClimbingType.Type;

                try
                {
                    await _climbingSessionService.SaveSession(_session);
                    await CoreMethods.PopPageModel(new SessionDetailResult(true, ECrud.Update), false, true);
                }
                catch (Exception ex)
                {
                    await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
                }
            }
        }

        async Task LoadData()
        {
            try
            {
                var result = await _subscriptionService.GetSubScriptions().ConfigureAwait(false);

                Subscriptions = result.ToList();

                if (_session != null)
                {
                    var subscription = Subscriptions.First(s => s.Id == _session.Subscription.Id);
                    DefaultSubscriptionValue = subscription.LblType;
                    SelectedSubscription = subscription;
                }
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }
        }

        #endregion
    }
}