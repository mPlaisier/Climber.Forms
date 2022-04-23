using FreshMvvm;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Acr.UserDialogs;

namespace Climber.Forms.Core
{
    public partial class App : Application
    {
        #region Constructor

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            InitializeServices();
            InitializeNavigation();
        }

        protected override void OnStart()
        {
            base.OnStart();

#if DEBUG
            AppCenter.Start("android=ad52d1cf-7df9-4325-a1dd-517dff0d7699;" +
                            "ios=34001207-590e-4587-b15a-d5ff9884d9a0",
                            typeof(Analytics), typeof(Crashes));
#else
            AppCenter.Start("android=42e8ebb0-974a-4b2c-9091-c2821a0e985d;" +
                           "ios=2d1f238d-97da-4332-a4ad-1c491f37f322",
                           typeof(Analytics), typeof(Crashes));
#endif
        }

        #endregion

        #region Private

        void InitializeServices()
        {
            //UserDialogs/Messages
            FreshIOC.Container.Register(UserDialogs.Instance);
            FreshIOC.Container.Register<IToastMessageService, ToastMessageService>();
            FreshIOC.Container.Register<IDialogService, DialogService>();
            FreshIOC.Container.Register<IMessageService, MessageService>();

            //Db
            FreshIOC.Container.Register<IDatabaseService, LocalDatabase>();

            //Climbing services
            FreshIOC.Container.Register<IClimbingTaskService, ClimbingTaskService>();

            FreshIOC.Container.Register<IClimbingSessionService, ClimbingSessionService>();
            FreshIOC.Container.Register<ISubscriptionService, SubscriptionService>();
            FreshIOC.Container.Register<IEquipmentService, EquipmentService>();
            FreshIOC.Container.Register<IClimbingClubService, ClimbingClubService>();
            FreshIOC.Container.Register<IClimbingGradeService, ClimbingGradeService>();

            //Dashboard
            FreshIOC.Container.Register<IDashboardSubscriptionScreenManagerService, DashboardSubscriptionScreenManagerService>();
            FreshIOC.Container.Register<IDashboardService, DashboardService>();
        }

        void InitializeNavigation()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu");

            masterDetailNav.AddPage<DashboardViewModel>(LblDashboard.Dashboard_Title, null);
            masterDetailNav.AddPage<ClimbingSessionsViewModel>(Labels.Session_Overview_Title, null);
            masterDetailNav.AddPage<SubscriptionViewModel>(Labels.Subscription_Title, null);
            masterDetailNav.AddPage<EquipmentOverviewViewModel>(Labels.Equipment_Title, null);
            masterDetailNav.AddPage<ClimbingClubOverviewViewModel>(Labels.Club_Title, null);

            MainPage = masterDetailNav;
        }

        #endregion
    }
}