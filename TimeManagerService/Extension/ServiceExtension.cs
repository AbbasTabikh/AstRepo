using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TimeRecorder.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddTimeRecorder(this IServiceCollection services)
        {
            services.AddTransient<ITimeRecorderService, TimeRecorderService>();
            return services;
        }
    }
}
