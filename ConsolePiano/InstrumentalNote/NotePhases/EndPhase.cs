using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class EndPhase : Phase
    {
        public override double Duration => 0.0;

        protected override double Strength => 0.0;

        public override double Lowerlimit { get; set; }
        public override double UpperLimit { get; set; }

        //private ReleasePhase releasePhase;

        public EndPhase()
        {
        }
        public EndPhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote = 0;
        }
        //TODO get rid of this
        protected override bool IsNextPhase(int limit)
        {
            throw new NotImplementedException();
        }

        protected override void SetPhase()
        {
            throw new NotImplementedException();
        }
    }
}
