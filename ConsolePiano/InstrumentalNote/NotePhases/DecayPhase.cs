using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DecayPhase : Phase
    {
        public DecayPhase(Phase phase)
        {
            this.actualSound = phase.CurrentNote;
            this.instrument = phase.Instrument;
            Initialize();
        }

        public DecayPhase(double actualSound, DefaultInstrumentNote instrument)
        {
            this.actualSound = actualSound;
            this.instrument = instrument;
            Initialize();
        }

        private void Initialize()
        {
            strength = 18000.0;
            duration = 980.0;

            lowerlimit = 441.0; //Set State with prev. limit - need mechanism to ensure order of the phases
            upperLimit = 1421.0;
        }

        public override void NextNote(int limit)
        {
            actualSound -= strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit > upperLimit)
                instrument.Phase = new SustainPhase(this);
        }
    }
}

