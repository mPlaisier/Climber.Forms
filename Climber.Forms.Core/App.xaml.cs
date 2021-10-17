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
        }

        void InitializeNavigation()
        {
            var page = FreshPageModelResolver.ResolvePageModel<ClimbingSessionsViewModel>();
            var basicNavContainer = new FreshNavigationContainer(page)
            {
                BarBackgroundColor = Color.FromHex("880e4f"),
                BarTextColor = Color.White
            };

            MainPage = basicNavContainer;
        }

        #endregion
    }
}
