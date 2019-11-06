using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class DecayPhase : Phase
    {
        public DecayPhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
            Initialize();
        }

        private void Initialize()
        {
            strength = 18000.0;
            duration = 980.0;

            lowerlimit = 441.0; //Set State with prev. limit - need mechanism to ensure order of the phases
            upperLimit = 1421.0;
        }

        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote -= strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit > upperLimit)
                defaultInstrumentNote.Phase = defaultInstrumentNote.SustainPhase;//new SustainPhase(this);
        }
    }
}

