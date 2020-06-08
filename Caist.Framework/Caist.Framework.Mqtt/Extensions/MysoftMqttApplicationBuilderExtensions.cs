using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Caist.Framework.Mqtt.Extensions
{
    public static class MysoftMqttApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMqttClient(this IApplicationBuilder app)
        {
            //激活客户端
            var client = app.ApplicationServices.GetService<IMysoftMqttClient>();

            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();

            var cancellationToken = lifetime.ApplicationStopping;
            cancellationToken.Register(() =>
            {
                client.Dispose();
            });

            return app;
        }
    }
}
