using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class AddEditProfileViewModel
    {
        private readonly INavigationService _navigationService;

        public AddEditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SaveEditTapCommand => new Command(SaveEditTap);

        private async void SaveEditTap()
        {
            await _navigationService.NavigateAsync("MainListPage");
        }
    }
}
