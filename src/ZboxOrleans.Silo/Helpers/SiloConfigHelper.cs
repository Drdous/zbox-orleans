using ZboxOrleans.Silo.Config;

namespace ZboxOrleans.Silo.Helpers;

public static class SiloConfigHelper
{
    public static SiloEndpointConfig GetSiloEndpointConfig(string[]? args)
    {
        if (args is not {Length: 2} || !int.TryParse(args[0], out var siloPort) || !int.TryParse(args[1], out var gatewayPort))
        {
            return new SiloEndpointConfig(11111, 30000);
        }

        return new SiloEndpointConfig(siloPort, gatewayPort);
    }
}