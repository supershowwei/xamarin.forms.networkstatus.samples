using System;
using System.Net;
using SystemConfiguration;
using CoreFoundation;

namespace NetworkStatusApp.iOS
{
    internal enum NetworkStatus
    {
        NotReachable,
        ReachableViaCarrierDataNetwork,
        ReachableViaWiFiNetwork
    }

    internal static class Reachability
    {
        private static NetworkReachability defaultRouteReachability;

        // Raised every time there is an interesting reachable event,
        // we do not even pass the info as to what changed, and
        // we lump all three status we probe into one
        public static event EventHandler ReachabilityChanged;

        public static NetworkStatus InternetConnectionStatus()
        {
            var defaultNetworkAvailable = IsNetworkAvailable(out var flags);

            if (defaultNetworkAvailable && (flags & NetworkReachabilityFlags.IsDirect) != 0)
            {
                return NetworkStatus.NotReachable;
            }

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return NetworkStatus.ReachableViaCarrierDataNetwork;
            }

            if (flags == 0)
            {
                return NetworkStatus.NotReachable;
            }

            return NetworkStatus.ReachableViaWiFiNetwork;
        }

        private static bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            // Is it reachable with the current network configuration?
            var isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

            // Do we need a connection to reach it?
            var noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0
                                       || (flags & NetworkReachabilityFlags.IsWWAN) != 0;

            return isReachable && noConnectionRequired;
        }

        private static void OnChange(NetworkReachabilityFlags flags)
        {
            ReachabilityChanged?.Invoke(null, EventArgs.Empty);
        }

        private static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (defaultRouteReachability == null)
            {
                var ipAddress = new IPAddress(0);
                defaultRouteReachability = new NetworkReachability(ipAddress);
                defaultRouteReachability.SetNotification(OnChange);
                defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }

            return defaultRouteReachability.TryGetFlags(out flags) && IsReachableWithoutRequiringConnection(flags);
        }
    }
}