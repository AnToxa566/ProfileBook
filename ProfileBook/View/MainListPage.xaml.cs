using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListPage : ContentPage
    {
        public MainListPage()
        {
            InitializeComponent();
            Title = "Main List";
            NavigationPage.SetHasBackButton(this, false);
        }
    }
}