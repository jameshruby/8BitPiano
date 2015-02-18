using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class EndState : State
    {
        private ReleasePhase releasePhase;

        public EndState(ReleasePhase releasePhase)
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
