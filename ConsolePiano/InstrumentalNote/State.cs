using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote

{
    abstract class State
    {
        protected DefaultInstrumentNote instrument;
        protected double actualSound;

        protected double duration;
        protected double lowerlimit;
        protected double upperLimit;
        protected double strength;

        public DefaultInstrumentNote Instrument { get { return instrument; } set { instrument = value; } }
        public double CurrentNote { get { return actualSound; } set { actualSound = value; } }

        public abstract void NextNote(int limit);

    }
}
