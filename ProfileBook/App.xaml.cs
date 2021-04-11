﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using Prism.Ioc;
using ProfileBook.View;
using ProfileBook.ViewModel;

namespace ProfileBook
{
    public partial class App : PrismApplication
    {
        public App()
        {
        }

        #region ---Overrides---

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SingInPage, SingInViewModel>();
            containerRegistry.RegisterForNavigation<SingUpPage, SingUpViewModel>();
            containerRegistry.RegisterForNavigation<MainListPage, MailListViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfilePage, AddEditProfileViewModel>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var result = await NavigationService.NavigateAsync("NavigationPage/SingInPage");
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

    }
}
