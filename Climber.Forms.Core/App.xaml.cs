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

            InitializeServices();
            InitializeNavigation();
        }

        #endregion

        #region Private

        void InitializeServices()
        {
            FreshIOC.Container.Register<IDatabaseService, DummyDatabase>();
            FreshIOC.Container.Register<IClimbingSessionService, ClimbingSessionService>();
            FreshIOC.Container.Register<ISubscriptionService, SubscriptionService>();
        }

        void InitializeNavigation()
        {
            var mainPage = new FreshTabbedNavigationContainer()
            {
                BarBackgroundColor = Color.FromHex("880e4f"),
                BarTextColor = Color.White,
            };

            mainPage.AddTab<ClimbingSessionsViewModel>("Sessions", null);
            mainPage.AddTab<SubscriptionViewModel>("Subscriptions", null);

            MainPage = mainPage;
        }

        #endregion
    }
}