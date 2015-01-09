﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano
{
    interface IBeatController
    {
        View View { get;}

        void PerformActionWithStrategy(int tone);

        void GetTopEmloee();

        void Stop();
    }
}
