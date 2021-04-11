using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class MailListViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public MailListViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ProfileList = new ObservableCollection<Profile>()
            {
                new Profile
                {
                    NickName = "Vasya",
                    Name = "Vasiliy",
                    ImagePath = "profile.png",
                    CreationTime = DateTime.Now
                }
            };
        }

        #region ---Property---

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        private DateTime _creationTime;
        public DateTime CreationTime
        {
            get => _creationTime;
            set => SetProperty(ref _creationTime, value);
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
            await _navigationService.NavigateAsync("AddEditProfilePage");
        }

        private async void EditTap()
        {
            
        }

        private async void DeleteTap()
        {
            
        }

        #endregion
    }
}
