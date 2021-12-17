using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Climber.Forms.Core
{
    public class ClimbingClubOverviewViewModel : BaseViewModel
    {
        readonly IClimbingClubService _clubService;
        readonly IMessageService _messageService;
        readonly ClimbingTaskService _taskService;

        #region Properties

        public override string Title => Labels.Club_Overview_Title;

        public ObservableCollection<ClimbingClub> Clubs { get; private set; }

        #endregion

        #region Commands

        IAsyncCommand _commandAddEquipment;
        public IAsyncCommand CommandAddClub => _commandAddEquipment ??= new AsyncCommand(async () =>
        {
            await CoreMethods.PushPageModel<ClimbingClubDetailViewModel>();
        });

        #endregion

        #region Constructor

        public ClimbingClubOverviewViewModel(IClimbingClubService clubService, IMessageService messageService, ClimbingTaskService taskService)
        {
            _clubService = clubService;
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

            if (returnedData is CrudResult result && result.IsSuccess)
            {
                if (result.Action == ECrud.Create)
                    _messageService.ShowInfoMessage(Labels.Club_Overview_Alert_Created_Body, EMessagePriority.Low);
                else if (result.Action == ECrud.Update)
                    _messageService.ShowInfoMessage(Labels.Club_Overview_Alert_Updated_Body, EMessagePriority.Low);
                else if (result.Action == ECrud.Delete)
                    _messageService.ShowInfoMessage(Labels.Club_Overview_Alert_Deleted_Body, EMessagePriority.Medium);

                Init();
            }
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            IEnumerable<ClimbingClub> data = null;

            await _taskService.Execute(async () =>
            {
                data = await _clubService.GetClubs();
            });

            if (data != null)
            {
                var clubs = new ObservableCollection<ClimbingClub>(data);

                clubs.ForEach((club) =>
                {
                    club.ActionClicked = () =>
                    {
                        CoreMethods.PushPageModel<ClimbingClubDetailViewModel>(club);
                    };
                });

                Clubs = clubs;
            }
        }

        #endregion
    }
}