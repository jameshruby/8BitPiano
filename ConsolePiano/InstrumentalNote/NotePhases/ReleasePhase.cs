using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class ReleasePhase : Phase
    {
        //protected override double Duration => (double)1 / 6;
        public override double Duration => 1470.0;
        protected override double Lowerlimit => 3626.0;
        protected override double UpperLimit => 5831.0;
        protected override double Strength => 14000.0;

        public ReleasePhase() { }
        public ReleasePhase(DefaultInstrumentNote defaultInstrumentNote)
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
            if (limit > Lowerlimit && limit < UpperLimit)
            {
                defaultInstrumentNote.Phase = defaultInstrumentNote.SustainPhase; //new SustainPhase(this);
            }
            
            if (limit > UpperLimit && defaultInstrumentNote.CurrentNote <= 0)
                defaultInstrumentNote.Phase = defaultInstrumentNote.EndPhase; //new EndPhase(this);
        }
    }
}

