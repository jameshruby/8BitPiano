using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class ReleasePhase : Phase
    {
        public override double Duration => (double)1 / 6; // (double)1 / 6 1470.0
        public override double Lowerlimit { get; set; } // 3626.0;
        public override double UpperLimit { get; set; } //5096 5831.0;

        protected override double Strength => 14000.0;

        public ReleasePhase() { }
        public ReleasePhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote -= Strength / DurationSampled;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit+4 >= UpperLimit && defaultInstrumentNote.CurrentNote <= 0) //TODO FIX subobt. relies on the loop be
            {
                defaultInstrumentNote.Phase = defaultInstrumentNote.EndPhase; //new EndPhase(this);
            }
        }
    }
}

