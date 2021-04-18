using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MainListViewModel : BindableBase, IPageLifecycleAware
    {
        private INavigationService _navigationService;
        private IRepository _repository;

        public MainListViewModel(INavigationService navigationService, IRepository repository)
        {
            _navigationService = navigationService;
            _repository = repository;
        }

        #region ---Public Methods---

        public async void OnAppearing()
         {
            var profileList = await _repository.GetAllAsync<Profile>();
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
            await _navigationService.NavigateAsync("/NavigationPage/SingInPage");
        }

        private async void AddProfileTap()
        {
            foreach (var item in ProfileList)
            {
                item.IsSelected = false;
                await _repository.UpdateAsync(item);
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
                await _repository.UpdateAsync(item);
            }

            profile.IsSelected = true;
            await _repository.UpdateAsync(profile);

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
                await _repository.DeletAsync(profile);
            }

            if (ProfileList.Count == 0)
                NoProfilesAdded = "No profiles added.";
        }

        #endregion
    }
}
