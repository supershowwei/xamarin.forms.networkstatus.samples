namespace NetworkStatusApp.Protocol
{
    public interface IDeviceService
    {
        bool IsNetworkAvailable { get; }
    }
}