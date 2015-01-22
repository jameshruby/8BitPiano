using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano
{
     interface IBeatController
    {
        View ViewProp { get;}

        void PerformActionWithStrategy(int tone);

        void Stop();
    }
}
