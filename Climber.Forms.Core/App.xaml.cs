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
            var mainPage = new FreshTabbedFONavigationContainer("Climber")
            {
                BarBackgroundColor = Color.FromHex("880e4f"),
                BarTextColor = Color.White,
            };

            mainPage.AddTab<SubscriptionViewModel>(Labels.Subscription_Title, null);
            mainPage.AddTab<ClimbingSessionsViewModel>("Sessions", null);
            mainPage.AddTab<EquipmentOverviewViewModel>(Labels.Equipment_Title, null);

            MainPage = mainPage;
        }

        #endregion
    }
}