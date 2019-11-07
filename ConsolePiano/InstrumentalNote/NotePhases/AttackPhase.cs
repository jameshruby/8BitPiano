using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class AttackPhase : Phase
    {
        //Changing duration from direct values to 0..1 range

        public override double Duration => (double)1 / 20;//0.472; //(double)1 / 2;
        //public override double Duration => 441.0;
        protected override double Strength => 32767.0;

        public override double Lowerlimit { get; set; }
        public override double UpperLimit { get; set; }


        //these should be fields again. as we will calculate em


        //public override double UpperLimit => Duration;  //computed with samplesrate
        // public AttackPhase(DefaultInstrumentNote instrument) : base(instrument) { }

        /*
            It would be NICE to precompute PhaseDuration in ctor, but at least now, 
            we dont have durations for every phase until all the classes are created
         */
        public AttackPhase()
        {
            Lowerlimit = 0.0;
            // UpperLimit => Duration; 
        }
        public AttackPhase(DefaultInstrumentNote instrument)
        {
            this.defaultInstrumentNote = instrument;
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote += Strength / this.DurationSampled;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            //the other thing is i should maybe realy use percentages for both - strenth and samples
            if (limit+1 >= UpperLimit)  //TODO FIX subobt. relies on the loop be
            {
                defaultInstrumentNote.Phase = defaultInstrumentNote.DecayPhase;
                //flip the table
                //this means that we dont have actual values befoe we start to loop the sequencer :(
                defaultInstrumentNote.Phase.Lowerlimit = UpperLimit;
                defaultInstrumentNote.Phase.UpperLimit += DurationSampled;
            }
        }
    }
}