using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DefaultInstrumentNote
    {
        private double actualSound = 0.0;
        private Phase phase;

        public DefaultInstrumentNote()
        {
            AttackPhase = new AttackPhase(this);
            DecayPhase = new DecayPhase(this);
            SustainPhase = new SustainPhase(this);
            ReleasePhase = new ReleasePhase(this);
            EndPhase = new EndPhase(this);

            this.phase = AttackPhase;
        }

        public Phase Phase { get { return phase; } set { phase = value; } }
        public double CurrentNote { get { return actualSound; } set { actualSound = value; } }

        internal void ToNextNote(int limit) //not sure bout naming
        {
            phase.NextNote(limit);
        }
    }
}
