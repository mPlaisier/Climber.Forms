using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyChanged;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class ClimbingSessionDetailViewModel : BaseViewModel<ClimbingSession>
    {
        readonly IClimbingSessionService _climbingSessionService;
        readonly ISubscriptionService _subscriptionService;
        readonly IClimbingClubService _clubService;

        readonly IClimbingTaskService _taskService;

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

        Subscription _selectedSubscription;
        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled), nameof(IsClubEnabled))]
        public Subscription SelectedSubscription
        {
            get => _selectedSubscription;
            set
            {
                _selectedSubscription = value;
                if (_selectedSubscription != null && !_selectedSubscription.IsProtected)
                {
                    if (SelectedClub != _selectedSubscription.Club)
                    {
                        DefaultClubValue = _selectedSubscription.Club?.Name;
                        SelectedClub = _selectedSubscription.Club;
                    }
                }
                else if (_selectedSubscription != null)
                {
                    DefaultClubValue = string.Empty;
                    SelectedClub = null;
                }
            }
        }

        //Club
        public string ClubPlaceholder => Labels.Session_Detail_Club_Placeholder;

        public List<ClimbingClub> Clubs { get; private set; }
        public string DefaultClubValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public ClimbingClub SelectedClub { get; set; }

        //If user selct Single entree the user can select the club
        public bool IsClubEnabled => SelectedSubscription != null && SelectedSubscription.IsProtected;

        //Climbing Type
        public string ClimbingTypePlaceholder => Labels.Session_Detail_Climbing_Type_Placeholder;

        public List<ClimbingType> ClimbingTypes => ClimbingType.GetClimbingTypes();

        public string DefaultClimbingTypeValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public ClimbingType SelectedClimbingType { get; set; }

        //Climbing Grade
        public string ClimbingGradePlaceholder => Labels.Session_Detail_Climbing_Grade_Placeholder;

        public List<ClimbingGrade> ClimbingGrades => ClimbingGrade.GetValidClimbingGrades();

        public string DefaultClimbingGradeValue { get; set; }

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public ClimbingGrade SelectedClimbingGrade { get; set; }

        //Confirm button
        public string ConfirmButtonLabel => _session == null
                                                ? Labels.Session_Detail_Button_Create_Confirm
                                                : Labels.Session_Detail_Button_Update_Confirm;

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

        IAsyncCommand _commandConfirm;
        public IAsyncCommand CommandConfirm => _commandConfirm ??= new AsyncCommand(SaveSession, IsValid);

        IAsyncCommand _commandDeleteSession;
        public IAsyncCommand CommandDeleteSession => _commandDeleteSession ??= new AsyncCommand(DeleteSession);

        #endregion

        #region Constructor

        public ClimbingSessionDetailViewModel(IClimbingSessionService climbingSessionService,
                                              ISubscriptionService subscriptionService,
                                              IClimbingClubService clubService,
                                              IClimbingTaskService taskService)
        {
            _climbingSessionService = climbingSessionService;
            _subscriptionService = subscriptionService;
            _clubService = clubService;
            _taskService = taskService;
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

                //Climbing grade
                var grade = ClimbingGrade.GetAllClimbingGrades().First(s => s.Grade == _session.HighestGrade);
                DefaultClimbingGradeValue = grade.Label;
                SelectedClimbingGrade = grade;
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

            if (SelectedClub == null)
                return false;

            if (SelectedClimbingType == null)
                return false;

            if (SelectedClimbingGrade == null)
                return false;

            return true;
        }

        async Task SaveSession()
        {
            //Create
            if (_session == null)
            {
                var session = new ClimbingSession(SelectedDate.Value, SelectedSubscription, SelectedClub, SelectedClimbingType, SelectedClimbingGrade);

                await _taskService.Execute(async () =>
                {
                    await _climbingSessionService.SaveSession(session);
                    await CoreMethods.PopPageModel(new SessionDetailResult(true, ECrud.Create), false, true);
                });
            }
            else //Update
            {
                _session.Date = SelectedDate.Value;
                _session.Subscription = SelectedSubscription;
                _session.Club = SelectedClub;
                _session.Type = SelectedClimbingType.Type;
                _session.HighestGrade = SelectedClimbingGrade.Grade;

                await _taskService.Execute(async () =>
                {
                    await _climbingSessionService.SaveSession(_session);
                    await CoreMethods.PopPageModel(new SessionDetailResult(true, ECrud.Update), false, true);
                });
            }
        }

        async Task LoadData()
        {
            await _taskService.Execute(async () =>
            {
                var activeSubscriptions = await _subscriptionService.GetActiveSubscriptions().ConfigureAwait(false);
                Subscriptions = activeSubscriptions.ToList();

                var clubs = await _clubService.GetClubs().ConfigureAwait(false);
                Clubs = clubs.ToList();

                if (_session != null)
                {
                    //Subscription
                    if (!_session.Subscription.IsActive)
                        Subscriptions.Add(_session.Subscription);

                    DefaultSubscriptionValue = _session.Subscription.LblType;
                    SelectedSubscription = _session.Subscription;

                    //Club
                    if (_session.Club == null && SelectedSubscription.Club != null)
                    {
                        DefaultClubValue = SelectedSubscription.Club?.Name;
                        SelectedClub = SelectedSubscription.Club;
                    }
                    else
                    {
                        DefaultClubValue = _session.Club?.Name;
                        SelectedClub = _session.Club;
                    }
                }
            });
        }

        async Task DeleteSession()
        {
            if (_session != null)
            {
                await _taskService.ExecuteDelete(async () =>
                {
                    await _climbingSessionService.DeleteSession(_session);
                    await CoreMethods.PopPageModel(new SessionDetailResult(true, ECrud.Delete), false, true);
                });
            }
        }

        #endregion
    }
}