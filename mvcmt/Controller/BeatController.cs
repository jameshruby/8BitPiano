using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit8Piano
{
    class BeatController : IBeatController
    {
        IBeatModel beatModel;
        private View view;
        private bool monitoring;

        public BeatController(IBeatModel beatModel)
        {
            this.beatModel = beatModel;

            view = new View(this, beatModel);

            //this.beatModel.Attach((IEventObserver)view);
        }

        public View ViewProp
        {
            get
            {
                return view;
            }
        }

        public void PerformActionWithStrategy(int tone)
        {
            Tone actualTone = ToneStrategy(tone);
            beatModel.PlayTone(actualTone);
        }

        private static Tone ToneStrategy(int tone)
        {
            Tone actualTone;

            switch (tone)
            {
                case 1:
                    actualTone = Tone.C;
                    break;
                case 2:
                    actualTone = Tone.D;
                    break;
                case 3:
                    actualTone = Tone.E;
                    break;
                case 4:
                    actualTone = Tone.F;
                    break;
                case 5:
                    actualTone = Tone.G;
                    break;
                case 6:
                    actualTone = Tone.A;
                    break;
                case 7:
                    actualTone = Tone.B;
                    break;
                case 8:
                    actualTone = Tone.C5;
                    break;
                case 9:
                    actualTone = Tone.Csharp;
                    break;
                case 10:
                    actualTone = Tone.Dsharp;
                    break;
                case 11:
                    actualTone = Tone.Fsharp;
                    break;
                case 12:
                    actualTone = Tone.Gsharp;
                    break;
                case 13:
                    actualTone = Tone.Asharp;
                    break;
                default:
                    throw new NotSupportedException();
                    break;
            }
            return actualTone;
        }

        public void Stop()
        {
           
        }
    }
}
