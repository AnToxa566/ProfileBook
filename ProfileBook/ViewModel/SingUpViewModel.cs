using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModel
{
    public class SingUpViewModel
    {
        private readonly INavigationService _navigationService;

        public SingUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SingUpTapCommand => new Command(SingUpTap);

        private async void SingUpTap()
        {
            await _navigationService.NavigateAsync("/NavigationPage/SingInPage");
        }
    }
}
