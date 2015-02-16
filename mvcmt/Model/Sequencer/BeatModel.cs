using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bit8Piano
{
    class BeatModel : IBeatModel
    {
        public delegate void MyHandler<IBeatModel>(IBeatModel sender, EventArgs e);

        public event EventHandler tonePlayed;

        private const int DELAY = 1000;
        private string lastName;
        private string firstName;

        public event Action OnPropertyChange;
        private BeepNonStatic tonePlay;

        public BeatModel()
        {
            tonePlay = new BeepNonStatic();
            //MonitorChanges();
        }


        public void Attach(IEventObserver observerView)
        {
            tonePlay.Attach(observerView);
            //tonePlayed += new EventHandler<IBeatModel>(View.TonePlayed);
        }

        public void PlayTone(Tone actualTone)
        {
            //when i play sound in separate thread, i need to check if the same sound is not playing yet, if 
            //does so, im not calling play method

            tonePlay.Play((double)actualTone, 10);

            //Thread playToneThread = new Thread(() => PianoModel.PlayTone(actualTone, 10));
            //playToneThread.Start();
            //Sequencer.PlayTone(actualTone, Duration.QUARTER);
        }

       

        ~BeatModel()
        {
            tonePlay.Dispose();
        }
    }
}
