using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SingInViewModel : BindableBase, IPageLifecycleAware
    {
        private INavigationService _navigationService;
        private UserAsyncRepository _userRepository;

        public SingInViewModel(INavigationService navigationService, UserAsyncRepository repository)
        {
            _navigationService = navigationService;
            _userRepository = repository;

            SingInTapCommand = new Command(SingInTap, SingInAllowed);
            ToSingUpPageTapCommand = new Command(ToSingUpPageTap);
        }

        #region ---Appearing---

        public async void OnAppearing()
        {
            var userList = await _userRepository.GetAllAsync<User>();

            foreach (var item in userList)
            {
                if (item.IsAuthorization)
                {
                    await _navigationService.NavigateAsync("MainListPage");
                    break;
                }
            }
        }

        public void OnDisappearing(){}

        #endregion

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

        private bool _isUser;
        public bool IsUser
        {
            get => _isUser;
            set => SetProperty(ref _isUser, value);
        }

        #endregion

        #region ---Command---

        public Command SingInTapCommand { get; }

        public Command ToSingUpPageTapCommand { get; }

        #endregion

        #region ---Tap---

        private async void SingInTap(object obj)
        {
            var userList = await _userRepository.GetAllAsync<User>();
            IsUser = false;

            foreach (var item in userList)
            {
                if (_login == item.Login && _password == item.Password)
                {
                    IsUser = true;

                    item.IsAuthorization = true;
                    await _userRepository.UpdateAsync(item);

                    break;
                }
            }


            if (!IsUser)
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
