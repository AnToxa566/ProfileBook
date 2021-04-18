using System;
using Prism.Mvvm;
using Prism.AppModel;
using Prism.Navigation;
using Acr.UserDialogs;
using ProfileBook.Model;
using ProfileBook.Services.Repository;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class AddEditProfileViewModel : BindableBase, IPageLifecycleAware
    {
        private INavigationService _navigationService;
        private IProfileRepository _profileRepository;

        public AddEditProfileViewModel(INavigationService navigationService, IProfileRepository repository)
        {
            _navigationService = navigationService;
            _profileRepository = repository;

            Saved = false;
            FirstOnAppearing = true;
            ImagePath = "profile.png";
        }

        #region ---Public Methods---

        public async void OnAppearing()
        {
            var profileList = await _profileRepository.GetAllAsync<Profile>();
            ProfileList = new ObservableCollection<Profile>(profileList);

            foreach (var item in ProfileList)
            {
                if (item.IsSelected && FirstOnAppearing)
                {
                    NickName = item.NickName;
                    Name = item.Name;
                    Description = item.Description;
                    ImagePath = item.ImagePath;

                    Saved = true;
                    FirstOnAppearing = false;

                    break;
                }
            }
        }

        public void OnDisappearing()
        {
        }

        #endregion

        #region ---Command---

        public ICommand SaveEditTapCommand => new Command(SaveEditTap);

        public ICommand SetImageTapCommand => new Command(SetImageTap);

        #endregion

        #region ---Tap---

        private async void SaveEditTap()
        {
            if (string.IsNullOrEmpty(_nickName) || string.IsNullOrEmpty(_name))
                await UserDialogs.Instance.AlertAsync("NickName and Name must be filled!");
            else if (Saved)
            {
                foreach (var item in ProfileList)
                {
                    if (item.IsSelected == true)
                    {
                        item.NickName = NickName;
                        item.Name = Name;
                        item.Description = Description;
                        item.ImagePath = ImagePath;

                        await _profileRepository.UpdateAsync(item);

                        break;
                    }
                }

                await _navigationService.GoBackAsync();
            }
            else 
            {
                var profile = new Profile()
                {
                    NickName = NickName,
                    Name = Name,
                    Description = Description,
                    CreationTime = DateTime.Now,
                    ImagePath = ImagePath
                };

                var id = await _profileRepository.InsertAsync(profile);
                profile.Id = id;

                await _navigationService.GoBackAsync();
            }
        }

        private void SetImageTap()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                            .SetTitle("Set photo")
                            .Add("Take photo with camera", () => TakePhotoAsync(), "camera.png")
                            .Add("Pick at Gallery", () => GetPhotoAsync(), "galery.png")
                        );
        }

        async void TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"profile.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });

                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);

                ImagePath = photo.FullPath;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        async void GetPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                ImagePath = photo.FullPath;
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        #endregion

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

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set => SetProperty(ref _imagePath, value);
        }

        private ObservableCollection<Profile> _profileList;
        public ObservableCollection<Profile> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        private bool _saved;
        public bool Saved
        {
            get => _saved;
            set => SetProperty(ref _saved, value);
        }

        private bool _firstOnAppearing;
        public bool FirstOnAppearing
        {
            get => _firstOnAppearing;
            set => SetProperty(ref _firstOnAppearing, value);
        }

        #endregion

    }
}
