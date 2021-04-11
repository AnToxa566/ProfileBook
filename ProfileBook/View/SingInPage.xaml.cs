using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingInPage : ContentPage
    {
        public SingInPage()
        {
            InitializeComponent();
            Title = "Users SingIn";
        }
    }
}