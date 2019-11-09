using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    abstract public class Phase //TODO fix accessibility
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

        virtual public void NextNote(int limit)
        {
            SetNote();
            if (IsNextPhase(limit))
            {
                SetPhase();
                SetNextPhasePoperties();
            }
        }
        virtual protected void SetNote()
        {
            defaultInstrumentNote.CurrentNote += this.Strength / this.DurationSampled;
        }
        protected abstract bool IsNextPhase(int limit);
        protected abstract void SetPhase();
        virtual protected void SetNextPhasePoperties()
        {
            defaultInstrumentNote.Phase.Lowerlimit = this.UpperLimit;
            defaultInstrumentNote.Phase.UpperLimit += this.UpperLimit;
        }
    }
}
