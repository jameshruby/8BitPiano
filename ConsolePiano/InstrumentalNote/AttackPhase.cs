﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class AttackPhase : State
    {
        public AttackPhase(State state)
        {
            this.actualSound = state.CurrentNote;
            this.instrument = state.Instrument;
            Initialize();
        }

        public AttackPhase(double actualSound, DefaultInstrumentNote instrument)
        {
            this.actualSound = actualSound;
            this.instrument = instrument;
            Initialize();
        }

        private void Initialize()
        {
            strength = 32767.0;
            duration = 441.0;

            lowerlimit = 0.0;
            upperLimit = duration; //computed with samplesrate
        }

        public override void NextNote(int limit)
        {
            actualSound += strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            //the other thing is i should maybe realy use percentages for both - strenth and samples
            if (limit > upperLimit)
                instrument.State = new DecayPhase(this);
        }
    }
}

