using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano
{
    class DecayPhase : State
    {
        public DecayPhase(State state)
        {
            this.actualSound = state.CurrentNote;
            this.instrument = state.Instrument;
            Initialize();
        }

        public DecayPhase(double balance, DefaultInstrumentNote account)
        {
            this.actualSound = balance;
            this.instrument = account;
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
                instrument.State = new SustainPhase(this);
        }
    }
}

