using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProfileBook.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditProfilePage : ContentPage
    {
        public AddEditProfilePage()
        {
            InitializeComponent();
            Title = "Add Profile";
        }
    }
}