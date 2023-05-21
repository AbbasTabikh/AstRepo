using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMeasurer
{
    public interface ITimeMeasurerFactory
    {
        TimeMeasurer Create();
    }
}
