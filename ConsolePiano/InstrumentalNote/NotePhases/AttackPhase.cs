using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class AttackPhase : Phase
    {
        public override double Duration => (double)1 / 20;//0.472; //(double)1 / 2;
        protected override double Strength => 32767.0;
        public override double Lowerlimit { get; set; } //UpperLimit => Duration
        public override double UpperLimit { get; set; }

        public AttackPhase() { }
        public AttackPhase(DefaultInstrumentNote instrument)
        {
            this.defaultInstrumentNote = instrument;
        }

        protected override bool IsNextPhase(int limit)
        {
            //the other thing is i should maybe realy use percentages for both - strenth and samples
            //TODO FIX subobt. relies on the loop be
            return limit + 1 >= UpperLimit;
        }

        protected override void SetPhase()
        {
            defaultInstrumentNote.Phase = defaultInstrumentNote.DecayPhase;
        }
    }
}