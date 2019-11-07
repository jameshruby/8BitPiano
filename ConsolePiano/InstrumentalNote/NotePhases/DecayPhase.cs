using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DecayPhase : Phase
    {
        public override double Duration => (double)1 / 9; //980.0 (double)1 / 9

        //Set State with prev. limit - need mechanism to ensure order of the phases
        public override double Lowerlimit { get; set; } //441.0;
        public override double UpperLimit { get; set; } //1421.0;

        protected override double Strength => 14767; //14767 //18000.0

        public DecayPhase() {}
        public DecayPhase(DefaultInstrumentNote defaultInstrumentNote)
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
            if (limit+2 >= UpperLimit) //TODO FIX subobt. relies on the loop be
            {
                defaultInstrumentNote.Phase = defaultInstrumentNote.SustainPhase;//new SustainPhase(this);

                defaultInstrumentNote.Phase.Lowerlimit = UpperLimit;
                defaultInstrumentNote.Phase.UpperLimit += UpperLimit;
            }
        }
    }
}

