using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano.Model.Sequencer
{
    interface IPhase
    {
         double Duration { get; set; }
         double Strength { get; set; }
            
    }
}
