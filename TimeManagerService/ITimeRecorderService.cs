using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TimeRecorder
{
    public interface ITimeRecorderService
    {
        public string LoggingMessage { get; set; }

        void Start();
        void Stop();
    }
}
