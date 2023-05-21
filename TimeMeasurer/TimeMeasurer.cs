using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TimeMeasurer
{
    public class TimeMeasurer : IDisposable
    {
        private readonly ILogger _logger;
        private Stopwatch? stopwatch;


        private string _loggingMessage = string.Empty;
        public string LogginMessage
        {
            get { return _loggingMessage; }
            set 
            { 
                _loggingMessage = value;
                stopwatch!.Restart();
            }
        }




        public TimeMeasurer(ILogger logger)
        {
            _logger = logger;

            Start();
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

        private void Start()
        {
            stopwatch = Stopwatch.StartNew();
        }

    }
}