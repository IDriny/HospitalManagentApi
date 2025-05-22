using Serilog;

namespace HospitalManagentApi.Configuration
{
    public static class Hosts
    {
        public static IHostBuilder AddSerilog(this IHostBuilder host)
        {
            host.UseSerilog((ctx, LoggerConfiguration) =>
                LoggerConfiguration.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
            return host;
        }
    }
}
