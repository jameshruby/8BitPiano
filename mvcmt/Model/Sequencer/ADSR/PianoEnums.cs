using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano.Model.Sequencer.ADSR
{
    enum InstrumentDuration
    {
        Attack = 1 / 2,
        Decay = 1 / 9,
        Sustain = 1 / 4,
        Release = 1 / 6
    }

    enum InstrumentStrength
    {
        Attack = 32767,
        Decay = 18000,
        Sustain = 14000,
        Release = 0
    }
}