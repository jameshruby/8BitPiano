using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DefaultInstrumentNote
    {
        private State state;

        public DefaultInstrumentNote()
        {
            this.state = new AttackPhase(0.0, this);
        }

        public State State { get { return state; } set { state = value; } }
        public double CurrentNote { get { return state.CurrentNote; } }

        internal void ToNextNote(int limit) //not sure bout naming
        {
            state.NextNote(limit);
        }
    }
}
