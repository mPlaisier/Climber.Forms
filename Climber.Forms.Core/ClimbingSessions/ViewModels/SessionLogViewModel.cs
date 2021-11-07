using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class ClimbingSessionsViewModel : BaseViewModel
    {
        readonly IClimbingSessionService _climbingSessionService;

        #region Properties

        public override string Title => "Climbing Sessions";

        public ObservableCollection<ClimbingSessionItem> Sessions { get; private set; }

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

        #endregion

        #region private

        async Task LoadData()
        {
            var sessions = await _climbingSessionService.GetClimbingSessions();
            Sessions = new ObservableCollection<ClimbingSessionItem>(sessions.OrderByDescending(x => x.Data));
        }

        #endregion
    }
}