using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    abstract class Phase
    {
        abstract public double Duration { get; }
        abstract protected double Strength { get; }

        abstract public double Lowerlimit { get; set; }
        abstract public double UpperLimit { get; set; }

        protected DefaultInstrumentNote defaultInstrumentNote;
        protected double actualSound;

        public DefaultInstrumentNote Instrument { get { return defaultInstrumentNote; } set { defaultInstrumentNote = value; } }
        public double CurrentNote { get { return actualSound; } set { actualSound = value; } }

        public double DurationSampled { get; internal set; }

        public abstract void NextNote(int limit);

    }
}
