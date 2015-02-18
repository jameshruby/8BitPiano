﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class ReleasePhase : State
    {

        public ReleasePhase(State state)
            : this(state.CurrentNote, state.Instrument)
        {
        }

        public ReleasePhase(double actualSound, DefaultInstrumentNote instrument)
        {
            this.actualSound = actualSound;
            this.instrument = instrument;
            Initialize();
        }

        private void Initialize()
        {
            strength = 14000.0;
            duration = 1470.0;

            lowerlimit = 3626.0;
            upperLimit = 5831.0;
        }
       
        public override void NextNote(int limit)
        {
            actualSound -= strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit > lowerlimit && limit < upperLimit)
            {
                instrument.State = new SustainPhase(this);
            }
            
            if (limit > upperLimit && actualSound <= 0)
                instrument.State = new EndState(this);
        }
    }
}

