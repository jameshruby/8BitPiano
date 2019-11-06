using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DecayPhase : Phase
    {
        protected override double Duration => 980.0;
        protected override double Lowerlimit => 441.0; //Set State with prev. limit - need mechanism to ensure order of the phases
        protected override double UpperLimit => 1421.0;
        protected override double Strength => 18000.0;
        public DecayPhase(DefaultInstrumentNote defaultInstrumentNote)
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
                defaultInstrumentNote.Phase = defaultInstrumentNote.SustainPhase;//new SustainPhase(this);
        }
    }
}

