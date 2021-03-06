using Xamarin.Forms;
using Prism.Unity;
using Prism.Ioc;
using ProfileBook.View;
using ProfileBook.ViewModel;
using ProfileBook.Services.Repository;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        #region ---Overrides---

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IProfileRepository>(Container.Resolve<ProfileAsyncRepository>());
            containerRegistry.RegisterInstance<IUserRepository>(Container.Resolve<UserAsyncRepository>());

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SingInPage, SingInViewModel>();
            containerRegistry.RegisterForNavigation<SingUpPage, SingUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MainListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfileViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/SingInPage");
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

    }
}
