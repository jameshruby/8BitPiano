using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class SustainPhase : Phase
    {
        //protected override double Duration => (double)1 / 4;
        public override double Duration => 2205.0;
        protected override double Lowerlimit => 1421.0;
        protected override double UpperLimit => 3626.0;
        protected override double Strength => 0.0;

        public SustainPhase() { }
        public SustainPhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote -= Strength / Duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit > UpperLimit)
                defaultInstrumentNote.Phase = defaultInstrumentNote.ReleasePhase;// new ReleasePhase(this);
        }
    }
}
  