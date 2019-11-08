using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class SustainPhase : Phase
    {
        public override double Duration => (double)1 / 4; //(double)1 / 4 2205.0
        public override double Lowerlimit { get; set; } // 1421.0;
        public override double UpperLimit { get; set; } // 3626.0;
        protected override double Strength => -4000; //0.0 4000

        public SustainPhase() { }
        public SustainPhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
        }

        protected override bool IsNextPhase(int limit)
        {
            //TODO FIX subobt. relies on the loop be
            return limit + 3 >= UpperLimit;
        }

        protected override void SetPhase()
        {
            defaultInstrumentNote.Phase = defaultInstrumentNote.ReleasePhase;// new ReleasePhase(this);
        }
    }
}
  