using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class EndPhase : Phase
    {
        protected override double Duration => 0.0;
        protected override double Lowerlimit => 0.0;
        protected override double UpperLimit => 0.0;
        protected override double Strength => 0.0;

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
