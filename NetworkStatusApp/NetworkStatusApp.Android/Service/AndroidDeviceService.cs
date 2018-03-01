using Android.Content;
using Android.Net;
using NetworkStatusApp.Droid.Service;
using NetworkStatusApp.Protocol;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(AndroidDeviceService))]

namespace NetworkStatusApp.Droid.Service
{
    public class AndroidDeviceService : IDeviceService
    {
        public bool IsNetworkAvailable
        {
            get
            {
                var connectivityManager =
                    (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

                return connectivityManager.ActiveNetworkInfo != null
                       && connectivityManager.ActiveNetworkInfo.IsConnectedOrConnecting;
            }
        }
    }
}