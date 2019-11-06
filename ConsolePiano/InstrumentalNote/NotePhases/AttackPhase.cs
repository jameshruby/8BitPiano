using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class AttackPhase : Phase
    {
        public AttackPhase(DefaultInstrumentNote instrument)
        {
            this.defaultInstrumentNote = instrument;
            Initialize();
        }

        private void Initialize()
        {
            strength = 32767.0;
            duration = 441.0;

            lowerlimit = 0.0;
            upperLimit = duration; //computed with samplesrate
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote += strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            //the other thing is i should maybe realy use percentages for both - strenth and samples
            if (limit > upperLimit)
                defaultInstrumentNote.Phase = defaultInstrumentNote.DecayPhase;
        }
    }
}

