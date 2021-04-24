using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SingUpViewModel : BindableBase
    {
        private INavigationService _navigationService;
        private UserAsyncRepository _userRepository;

        public SingUpViewModel(INavigationService navigationService, UserAsyncRepository userRepository)
        {
            _navigationService = navigationService;
            _userRepository = userRepository;

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

        private bool _isLoginBusy;
        public bool IsLoginBusy
        {
            get => _isLoginBusy;
            set => SetProperty(ref _isLoginBusy, value);
        }

        #endregion

        #region ---Command---

        public Command SingUpTapCommand { get; }

        #endregion

        #region ---Tap---

        private async void SingUpTap(object obj)
        {
            var userList = await _userRepository.GetAllAsync<User>();
            IsLoginBusy = false;

            foreach (var item in userList)
            {
                if (_login == item.Login)
                {
                    await UserDialogs.Instance.AlertAsync("This login is already taken!");
                    IsLoginBusy = true;
                    break;
                }
            }

            if (!IsLoginBusy)
            {
                if (Login.Length < 4 || Login.Length > 16)
                    await UserDialogs.Instance.AlertAsync("Login must be at least 4 and no more than 16 characters!");
                else if (Password != ConfirmPassword)
                    await UserDialogs.Instance.AlertAsync("Password does not coincide!");
                else if (Password.Length < 8 || Password.Length > 16)
                    await UserDialogs.Instance.AlertAsync("Password must be at least 8 and no more than 16 characters!");
                else
                {
                    var user = new User
                    {
                        Login = Login,
                        Password = Password
                    };

                    await _userRepository.InsertAsync(user);
                    await _navigationService.NavigateAsync("/NavigationPage/SingInPage");
                }
            }
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
