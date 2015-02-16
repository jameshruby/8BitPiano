using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DefaultInstrumentNote
    {
        private Phase phase;

        public DefaultInstrumentNote()
        {
            this.phase = new AttackPhase(0.0, this);
        }

        public Phase Phase { get { return phase; } set { phase = value; } }
        public double CurrentNote { get { return phase.CurrentNote; } }

        internal void ToNextNote(int limit) //not sure bout naming
        {
            phase.NextNote(limit);
        }
    }
}
