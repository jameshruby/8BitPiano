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
                default:
                    actualTone = Tone.Fsharp1;
                    break;
            }
            return actualTone;
        }

         public void GetTopEmloee()
        {
            if (!monitoring)
            {
                monitoring = true;
                beatModel.MonitorChanges();
            }
        }

        public void Stop()
        {
            beatModel.Stop();
        }
    }
}
