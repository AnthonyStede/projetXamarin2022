using Xamarin.Forms;
using TimeTracker.Services;
using Storm.Mvvm;
using TimeTracker.Pages;

namespace TimeTracker
{
    public partial class App : MvvmApplication
    {
        public App() : base(() => new NavigationPage(new LoginPage()), RegisterServices)
        {
            InitializeComponent();
        }

        private static void RegisterServices()
        {
            DependencyService.RegisterSingleton<IApiService>(new ApiService());
        }


    }
}
