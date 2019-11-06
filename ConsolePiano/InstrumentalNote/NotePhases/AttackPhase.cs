using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class AttackPhase : Phase
    {
        //Changing duration from direct values to 0..1 range

        //protected override double Duration => 0.472; //(double)1 / 2;
        public override double Duration => 441.0;
        protected override double Strength => 32767.0;
        protected override double Lowerlimit => 0.0;
        protected override double UpperLimit => Duration;  //computed with samplesrate
        // public AttackPhase(DefaultInstrumentNote instrument) : base(instrument) { }
        public AttackPhase() {}
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

//var realDecayDuration = realAttackDuration + PhaseDuration(DecayPhase.duration);