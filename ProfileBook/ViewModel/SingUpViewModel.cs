using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SingUpViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public SingUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            SingUpTapCommand = new Command(SingUpTap, SingUpAllowed);
        }

        #region ---Property---

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                SingUpTapCommand.ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                SingUpTapCommand.ChangeCanExecute();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                SetProperty(ref _confirmPassword, value);
                SingUpTapCommand.ChangeCanExecute();
            }
        }

        #endregion

        #region ---Command---

        public Command SingUpTapCommand { get; }

        #endregion

        #region ---Tap---

        private async void SingUpTap(object obj)
        {
            if (Login.Length < 4 || Login.Length > 16)
                await UserDialogs.Instance.AlertAsync("Login must be at least 4 and no more than 16 characters!");
            else if (Password != ConfirmPassword)
                await UserDialogs.Instance.AlertAsync("Password does not coincide!");
            else if (Password.Length < 8 || Password.Length > 16)
                await UserDialogs.Instance.AlertAsync("Password must be at least 8 and no more than 16 characters!");
            else if (Login == "Anton") // TODO: Захардкоренная проверка на логин в БД 
                await UserDialogs.Instance.AlertAsync("This login is already taken!");
            else
                await _navigationService.NavigateAsync("/NavigationPage/SingInPage");
        }

        #endregion

        #region ---SingUpAllowed---

        public bool SingUpAllowed(object obj) => 
            !string.IsNullOrEmpty(_login) && 
            !string.IsNullOrEmpty(_password) && 
            !string.IsNullOrEmpty(_confirmPassword);

        #endregion

    }
}
