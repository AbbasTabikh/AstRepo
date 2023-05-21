using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMeasurer
{
    public class TimeMeasurerFactory : ITimeMeasurerFactory
    {
        private readonly ILogger _logger;

        public TimeMeasurerFactory(ILogger<TimeMeasurer> logger) 
        { 
            _logger = logger;
        }

        public TimeMeasurer Create()
        {
            return new TimeMeasurer(_logger);
        }
    }
}
