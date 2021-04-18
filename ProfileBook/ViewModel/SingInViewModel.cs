using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SingInViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public SingInViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SingInTapCommand = new Command(SingInTap, SingInAllowed);
            ToSingUpPageTapCommand = new Command(ToSingUpPageTap);
        }

        #region ---Property---

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                SingInTapCommand.ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                SingInTapCommand.ChangeCanExecute();
            }
        }

        #endregion

        #region ---Command---

        public Command SingInTapCommand { get; }

        public Command ToSingUpPageTapCommand { get; }

        #endregion

        #region ---Tap---

        private async void SingInTap(object obj)
        {
            // TODO: Хардкорная проверка пользователя
            if (_login != "q" || _password != "q")
            {
                await UserDialogs.Instance.AlertAsync("Invalid login or password!");
                Password = string.Empty;
            }
            else
                await _navigationService.NavigateAsync("MainListPage");
        }

        private async void ToSingUpPageTap()
        {
            await _navigationService.NavigateAsync("SingUpPage");
        }

        #endregion

        #region ---SingUpAllowed---

        public bool SingInAllowed(object obj) => 
            !string.IsNullOrEmpty(_login) && 
            !string.IsNullOrEmpty(_password);

        #endregion

    }
}
