using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SingInViewModel
    {
        private readonly INavigationService _navigationService;

        public SingInViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SingInTapCommand => new Command(SingInTap);

        public ICommand ToSingUpPageTapCommand => new Command(ToSingUpPageTap);

        private async void SingInTap()
        {
            await _navigationService.NavigateAsync("MainListPage");
        }

        private async void ToSingUpPageTap()
        {
            await _navigationService.NavigateAsync("SingUpPage");
        }
    }
}
