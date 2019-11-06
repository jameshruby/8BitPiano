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

        public double NoteDuration { get; private set; }

        public Phase AttackPhase { get; }
        public Phase DecayPhase { get; }
        public Phase SustainPhase { get; }
        public Phase ReleasePhase { get; }
        public Phase EndPhase { get; }

        public DefaultInstrumentNote()
        {
            AttackPhase = this.GetPhaseInstance<AttackPhase>();
            DecayPhase = this.GetPhaseInstance<DecayPhase>();
            SustainPhase = this.GetPhaseInstance<SustainPhase>();
            ReleasePhase = this.GetPhaseInstance<ReleasePhase>();
            EndPhase = this.GetPhaseInstance<EndPhase>();

            this.phase = AttackPhase;
        }

        public Phase GetPhaseInstance<T>() where T : Phase, new()
        {
            //Create instance and increment note duration
            Phase phase = new T();
            phase.Instrument = this;
            NoteDuration += phase.Duration;
            return phase;
        }

        public Phase Phase { get { return phase; } set { phase = value; } }
        public double CurrentNote { get { return actualSound; } set { actualSound = value; } }

        internal void ToNextNote(int limit) //not sure bout naming
        {
            phase.NextNote(limit);
        }
    }
}
