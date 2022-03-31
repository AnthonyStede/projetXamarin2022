using System.Windows.Input;
using TimeTracker.Pages;
using Xamarin.Forms;

namespace TimeTracker.ViewModels
{
    class MenuViewModel
    {

        public ICommand GoToProfilCommand => new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new ProfilPage()));
        public ICommand GoToProjectCommand => new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new MenuPage()));
    }
}