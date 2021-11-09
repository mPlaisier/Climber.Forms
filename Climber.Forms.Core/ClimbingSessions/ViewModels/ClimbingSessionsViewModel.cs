using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        ICommand _commandAddSession;
        public ICommand CommandAddSession => _commandAddSession ??= new Command(async () =>
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

                Init();
            }
        }

        #endregion

        #region private

        async Task LoadData()
        {
            var sessions = await _climbingSessionService.GetClimbingSessions();
            Sessions = new ObservableCollection<ClimbingSession>(sessions.OrderByDescending(x => x.Date));
        }

        #endregion
    }
}