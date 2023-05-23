using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TimeMeasurer
{
    public class TimeMeasurer : IDisposable
    {
        private readonly ILogger _logger;
        private readonly Stopwatch stopwatch;


        private string _loggingMessage = "Time Elapsed :";
        public string LogginMessage
        {
            private get => _loggingMessage;
            set
            {
                stopwatch.Stop();
                  _loggingMessage = value;
                stopwatch.Start();
            }
        }




        public TimeMeasurer(ILogger logger)
        {
            _logger = logger;
            stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            if(stopwatch == null)
            {
                return;
            }

            stopwatch.Stop();
            string loggingMessage = $"{_loggingMessage}: {stopwatch?.Elapsed.TotalSeconds}";
            _logger.LogInformation(loggingMessage);
        }

    }
}