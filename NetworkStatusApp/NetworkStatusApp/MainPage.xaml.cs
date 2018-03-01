using NetworkStatusApp.Protocol;
using Xamarin.Forms;

namespace NetworkStatusApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.Label1.Text = DependencyService.Get<IDeviceService>().IsNetworkAvailable.ToString();
        }
    }
}