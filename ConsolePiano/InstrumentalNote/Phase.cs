using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    abstract class Phase
    {
        abstract protected double Duration { get; }
        abstract protected double Lowerlimit { get; }
        abstract protected double UpperLimit { get; }
        abstract protected double Strength { get; }

        protected DefaultInstrumentNote defaultInstrumentNote;
        protected double actualSound;

        public DefaultInstrumentNote Instrument { get { return defaultInstrumentNote; } set { defaultInstrumentNote = value; } }
        public double CurrentNote { get { return actualSound; } set { actualSound = value; } }

        public abstract void NextNote(int limit);

    }
}
