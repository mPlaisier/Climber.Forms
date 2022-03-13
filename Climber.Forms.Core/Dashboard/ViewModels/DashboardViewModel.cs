using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class DashboardViewModel : BaseViewModel
    {
        readonly IDashboardService _dashboardService;

        #region Properties

        public override string Title => LblDashboard.Dashboard_Title;

        public ObservableCollection<ICell> Items { get; private set; }

        #endregion

        #region Commands

        IAsyncCommand _commandAddSession;
        public IAsyncCommand CommandAddSession => _commandAddSession ??= new AsyncCommand(async () =>
        {
            await CoreMethods.PushPageModel<ClimbingSessionDetailViewModel>();
        });

        #endregion

        #region Constructor

        public DashboardViewModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        #endregion

        #region Lifecycle

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            LoadData().ConfigureAwait(false);
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            Items = await _dashboardService.GetDashboardItems(CoreMethods);
        }

        #endregion
    }
}
