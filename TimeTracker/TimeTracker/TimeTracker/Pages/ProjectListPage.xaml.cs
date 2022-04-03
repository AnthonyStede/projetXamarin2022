using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Dtos;
using TimeTracker.Dtos.Projects;
using TimeTracker.Services;
using TimeTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectListPage : ContentPage
    {
        private ObservableCollection<ProjectItem> Projets;
        public ProjectListPage()
        {
            InitializeComponent();
            BindingContext = new ProjectViewModel();
        }
        async void CheckProjet(object sender, System.EventArgs e)
        {
            /*Button btn = (Button)sender;
            int idProjet = Convert.ToInt32(btn.ClassId.ToString());

            Device.BeginInvokeOnMainThread(async () =>
            {
                ApiService service = DependencyService.Get<ApiService>();
                ApiUtil util = DependencyService.Get<ApiUtil>();
                var accessToken = util.Access_token;
                Response<List<ProjectItem>> response = await service.ProjetsAsync(accessToken);
                if (response.IsSuccess)
                {
                    Projets = new ObservableCollection<ProjectItem>(response.Data);
                }

                foreach (ProjectItem s in Projets)
                {
                    if (s.Id == idProjet)
                    {
                        await Navigation.PushAsync(new TaskListPage(s.Name, s.Description, idProjet));
                    }
                }
            });*/
        }
    }
}