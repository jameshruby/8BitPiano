using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class EndPhase : Phase
    {
        //private ReleasePhase releasePhase;

        public EndPhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
        }

        public override void NextNote(int limit)
        {
            return;
        }
    }
}
