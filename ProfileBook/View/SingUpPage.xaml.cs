using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUpPage : ContentPage
    {
        public SingUpPage()
        {
            InitializeComponent();
            Title = "Users SingUp";
        }
    }
}