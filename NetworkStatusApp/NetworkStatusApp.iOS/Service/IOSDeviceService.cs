using NetworkStatusApp.iOS.Service;
using NetworkStatusApp.Protocol;

[assembly: Xamarin.Forms.Dependency(typeof(IOSDeviceService))]

namespace NetworkStatusApp.iOS.Service
{
    public class IOSDeviceService : IDeviceService
    {
        public bool IsNetworkAvailable => Reachability.InternetConnectionStatus() != NetworkStatus.NotReachable;
    }
}