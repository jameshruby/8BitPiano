﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class SustainPhase : Phase
    {
        public SustainPhase(Phase phase)
            : this(phase.CurrentNote, phase.Instrument)
        {
        }

        public SustainPhase(double balance, DefaultInstrumentNote account)
        {
            this.actualSound = balance;
            this.instrument = account;
            Initialize();
        }

        private void Initialize()
        {
            strength = 0.0;
            duration = 2205.0;

            lowerlimit = 1421.0;
            upperLimit = 3626.0;
        }

        public override void NextNote(int limit)
        {
            actualSound -= strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit > upperLimit)
                instrument.Phase = new ReleasePhase(this);
        }
    }
}