using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class EndPhase : Phase
    {
        private ReleasePhase releasePhase;

        public EndPhase(ReleasePhase releasePhase)
        {
            // TODO: Complete member initialization
            this.releasePhase = releasePhase;
        }
        public override void NextNote(int limit)
        {
            return;
        }
    }
}
