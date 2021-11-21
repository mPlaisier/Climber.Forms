using FreshMvvm;
using Xamarin.Forms;

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

        #endregion

        #region Private

        void InitializeServices()
        {
            //Db
            FreshIOC.Container.Register<IDatabaseService, LocalDatabase>();

            //Climbing services
            FreshIOC.Container.Register<IClimbingSessionService, ClimbingSessionService>();
            FreshIOC.Container.Register<ISubscriptionService, SubscriptionService>();
            FreshIOC.Container.Register<IEquipmentService, EquipmentService>();
        }

        void InitializeNavigation()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Menu");

            masterDetailNav.AddPage<ClimbingSessionsViewModel>(Labels.Session_Overview_Title, null);
            masterDetailNav.AddPage<SubscriptionViewModel>(Labels.Subscription_Title, null);
            masterDetailNav.AddPage<EquipmentOverviewViewModel>(Labels.Equipment_Title, null);

            MainPage = masterDetailNav;
        }

        #endregion
    }
}