using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class AttackPhase : Phase
    {
        protected override double Duration => 441.0;
        protected override double Strength => 32767.0;
        protected override double Lowerlimit => 0.0;
        protected override double UpperLimit => Duration;  //computed with samplesrate

        public AttackPhase(DefaultInstrumentNote instrument)
        {
            this.defaultInstrumentNote = instrument;
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote += Strength / Duration;
             StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            //the other thing is i should maybe realy use percentages for both - strenth and samples
            if (limit > UpperLimit)
                defaultInstrumentNote.Phase = defaultInstrumentNote.DecayPhase;
        }
    }
}

