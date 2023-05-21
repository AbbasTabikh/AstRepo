using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMeasurer.Extension
{
    public static class ServiceExtenesion
    {
        public static IServiceCollection AddTimeMeasurerFactory(this IServiceCollection services)
        {
            services.AddSingleton<ITimeMeasurerFactory, TimeMeasurerFactory>();
            return services;
        }
    }
}
