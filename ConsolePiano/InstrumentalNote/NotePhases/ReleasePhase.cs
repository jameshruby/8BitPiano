using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano.InstrumentalNote
{
    class ReleasePhase : Phase
    {
        public ReleasePhase(DefaultInstrumentNote defaultInstrumentNote)
        {
            this.defaultInstrumentNote = defaultInstrumentNote;
            Initialize();
        }

        private void Initialize()
        {
            strength = 14000.0;
            duration = 1470.0;

            lowerlimit = 3626.0;
            upperLimit = 5831.0;
        }
       
        public override void NextNote(int limit)
        {
            defaultInstrumentNote.CurrentNote -= strength / duration;
            StateChangeCheck(limit);
        }

        private void StateChangeCheck(int limit)
        {
            if (limit > lowerlimit && limit < upperLimit)
            {
                defaultInstrumentNote.Phase = defaultInstrumentNote.SustainPhase; //new SustainPhase(this);
            }
            
            if (limit > upperLimit && defaultInstrumentNote.CurrentNote <= 0)
                defaultInstrumentNote.Phase = defaultInstrumentNote.EndPhase; //new EndPhase(this);
        }
    }
}

