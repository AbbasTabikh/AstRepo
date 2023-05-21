using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TimeRecorder
{
    public class TimeRecorderService : ITimeRecorderService
    {
        
        private readonly ILogger _logger;
        private Stopwatch? stopwatch;



        public string LoggingMessage { get; set; } = "Time elapsed : ";

        public TimeRecorderService(ILogger<TimeRecorderService> logger) 
        {
            _logger = logger;
        }

        public void Start()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public void Stop()
        {
            if(stopwatch == null)
            {
                return;
            }

            stopwatch.Stop();

            string loggingMessage = $"{LoggingMessage}: {stopwatch?.Elapsed.TotalSeconds}";
            
            _logger.LogInformation(loggingMessage);

        }




    }
}
