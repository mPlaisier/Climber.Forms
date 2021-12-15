using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Climber.Forms.Core
{
    public class ClimbingSessionsViewModel : BaseViewModel
    {
        readonly IClimbingSessionService _climbingSessionService;

        #region Properties

        public override string Title => Labels.Session_Overview_Title;

        public ObservableCollection<ClimbingSession> Sessions { get; private set; }

        #endregion

        #region Commands

        IAsyncCommand _commandAddSession;
        public IAsyncCommand CommandAddSession => _commandAddSession ??= new AsyncCommand(async () =>
        {
            await CoreMethods.PushPageModel<ClimbingSessionDetailViewModel>();
        });

        #endregion

        #region Constructor

        public ClimbingSessionsViewModel(IClimbingSessionService climbingSessionService)
        {
            _climbingSessionService = climbingSessionService;
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

            if (returnedData is SessionDetailResult result && result.IsSuccess)
            {
                if (result.Action == ECrud.Create)
                    CoreMethods.DisplayAlert(Labels.Session_Alert_Created_Title, Labels.Session_Alert_Created_Body, Labels.Ok);
                if (result.Action == ECrud.Update)
                    CoreMethods.DisplayAlert(Labels.Session_Alert_Updated_Title, Labels.Session_Alert_Updated_Body, Labels.Ok);
                if (result.Action == ECrud.Delete)
                    CoreMethods.DisplayAlert(Labels.Session_Alert_Deleted_Title, Labels.Session_Alert_Deleted_Body, Labels.Ok);

                Init();
            }
        }

        #endregion

        #region private

        async Task LoadData()
        {
            IEnumerable<ClimbingSession> data = null;
            try
            {
                data = await _climbingSessionService.GetClimbingSessions();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }

            if (data != null)
            {
                var lstSessions = new ObservableCollection<ClimbingSession>(data);

                lstSessions.ForEach((session) =>
                {
                    session.ActionClicked = () =>
                    {
                        CoreMethods.PushPageModel<ClimbingSessionDetailViewModel>(session);
                    };
                });

                Sessions = lstSessions;
            }
        }

        #endregion
    }
}