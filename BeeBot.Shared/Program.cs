using BeeBot.Shared.BeeComponents;
using BeeBot.Shared.ControllersComponents;
using BeeBot.Shared.MapComponents;
using Microsoft.Extensions.DependencyInjection;

namespace BeeBot.Shared
{
    public static class Program
    {
        public static void AddBeeBot(this IServiceCollection services)
		{    
            services.AddSingleton<Map>();
            services.AddSingleton<PlayingArea>();
            services.AddSingleton<Bee>();
            services.AddSingleton<ControllerCommands>();
            services.AddSingleton<ControllerMap>();
		}
    }
}
