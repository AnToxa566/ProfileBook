using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : ContentPage
    {
        public SignInView()
        {
            InitializeComponent();
            Title = "Users SingIn";
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SingUpView());
        }
    }
}