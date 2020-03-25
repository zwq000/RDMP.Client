#if NETSTANDARD_21
using Enbiso.NLib.EventBus;
using Microsoft.Extensions.DependencyInjection;
using RMDP.Handlers;

namespace RMDP {
    public static class ServiceExtensions {

        public static void AddRmdpEventHandler (this IServiceCollection services) {
            services.AddScoped<IEventHandler, GgdsMsgEventHandler> ();
            services.AddScoped<IEventHandler, VdatMsgEventHandler> ();
        }
    }
}
#endif