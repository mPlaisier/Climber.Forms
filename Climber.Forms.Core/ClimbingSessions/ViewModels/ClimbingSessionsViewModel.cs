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
        readonly IMessageService _messageService;
        readonly ClimbingTaskService _taskService;

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

        public ClimbingSessionsViewModel(IClimbingSessionService climbingSessionService, IMessageService messageService, ClimbingTaskService taskService)
        {
            _climbingSessionService = climbingSessionService;
            _messageService = messageService;
            _taskService = taskService;
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
                    _messageService.ShowInfoMessage(Labels.Session_Alert_Created_Body, EMessagePriority.Low);
                if (result.Action == ECrud.Update)
                    _messageService.ShowInfoMessage(Labels.Session_Alert_Updated_Body, EMessagePriority.Low);
                if (result.Action == ECrud.Delete)
                    _messageService.ShowInfoMessage(Labels.Session_Alert_Deleted_Body, EMessagePriority.Medium);

                Init();
            }
        }

        #endregion

        #region private

        async Task LoadData()
        {
            IEnumerable<ClimbingSession> data = null;

            await _taskService.Execute(async () =>
            {
                data = await _climbingSessionService.GetClimbingSessions();
            });

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