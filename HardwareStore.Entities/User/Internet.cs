using System.Runtime.InteropServices;

namespace HardwareStore.Entities.User;

public static class Internet
{
    [DllImport("wininet.dll")]
    private extern static bool InternetGetConnectedState(out int description, int reservedValue);

    public static bool IsAvailable()
        => InternetGetConnectedState(out int _, 0);
}