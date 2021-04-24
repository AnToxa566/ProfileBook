using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainListViewModel : BindableBase, IPageLifecycleAware
    {
        private INavigationService _navigationService;
        private ProfileAsyncRepository _profileRepository;
        private UserAsyncRepository _userRepository;

        public MainListViewModel(INavigationService navigationService, ProfileAsyncRepository profileRepository, UserAsyncRepository userRepository)
        {
            _navigationService = navigationService;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        #region ---Appearing---

        public async void OnAppearing()
        {
            var userList = await _userRepository.GetAllAsync<User>();
            foreach (var item in userList)
            {
                if (item.IsAuthorization)
                {
                    UserID = item.Id;
                    break;
                }
            }

            var profiles = await _profileRepository.GetAllAsync<Profile>();
            var profileList = profiles.Where(item => item.UserId == UserID);

            ProfileList = new ObservableCollection<Profile>(profileList);

            if (ProfileList.Count == 0)
                NoProfilesAdded = "No profiles added.";
            else
                NoProfilesAdded = string.Empty;

            foreach (var item in ProfileList)
            {
                item.IsSelected = false;
                item.ListViewModel = this;
            }
        }

        public void OnDisappearing()
        {
        }

        #endregion

        #region ---Property---

        private string _noProfilesAdded;
        public string NoProfilesAdded
        {
            get => _noProfilesAdded;
            set => SetProperty(ref _noProfilesAdded, value);
        }

        private ObservableCollection<Profile> _profileList;
        public ObservableCollection<Profile> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        private int _userId;
        public int UserID
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        #endregion

        #region ---Command---

        public ICommand LogOutTapCommand => new Command(LogOutTap);

        public ICommand AddProfileTapCommand => new Command(AddProfileTap);

        public ICommand EditTapCommand => new Command(EditTap);

        public ICommand DeleteTapCommand => new Command(DeleteTap);

        #endregion

        #region ---Tap---

        private async void LogOutTap()
        {
            var userList = await _userRepository.GetAllAsync<User>();

            foreach (var item in userList)
            {
                if (item.IsAuthorization)
                {
                    item.IsAuthorization = false;
                    await _userRepository.UpdateAsync(item);

                    await _navigationService.NavigateAsync("/NavigationPage/SingInPage");
                    break;
                }
            }
        }

        private async void AddProfileTap()
        {
            foreach (var item in ProfileList)
            {
                item.IsSelected = false;
                await _profileRepository.UpdateAsync(item);
            }

            await _navigationService.NavigateAsync("AddEditProfilePage");
        }

        private async void EditTap(object profileObj)
        {
            Profile profile = profileObj as Profile;

            if (profile == null)
                return;

            foreach (var item in ProfileList)
            {
                item.IsSelected = false;
                await _profileRepository.UpdateAsync(item);
            }

            profile.IsSelected = true;
            await _profileRepository.UpdateAsync(profile);

            await _navigationService.NavigateAsync("AddEditProfilePage");
        }

        private async void DeleteTap(object profileObj)
        {
            Profile profile = profileObj as Profile;

            if (profile == null) 
                return;

            var delete = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to delete the profile?",
                "Delete profile", "Yes", "No");

            if (delete)
            {
                ProfileList.Remove(profile);
                await _profileRepository.DeletAsync(profile);
            }

            if (ProfileList.Count == 0)
                NoProfilesAdded = "No profiles added.";
        }

        #endregion
    }
}
