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
        
        private static readonly string[] firstNames = new[] { "Jane", "Jack", "Joe" };
        private static readonly string[] lastNames = new[] { "Doe", "Black", "White" };
        private static readonly Random rand = new Random();

        private const int DELAY = 1000;
        private string lastName;
        private string firstName;

        public event Action OnPropertyChange;
        private BeepNonStatic tonePlay;

        private void StartMonitoringChanges()
        {
            tonePlay = new BeepNonStatic();

            while (true)
            {
                //FirePropertyChange();
                //SetEmployeeTo
                //(
                //    firstNames[rand.Next(firstNames.Length)],
                //    lastNames[rand.Next(lastNames.Length)]
                //);
                //Thread.Sleep(DELAY);
            }
        }

        private void SetEmployeeTo(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            FirePropertyChange();
        }

        private void FirePropertyChange()
        {
            var propChange = OnPropertyChange;
            if (propChange != null)
            {
                OnPropertyChange();
            }
        }

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

        public void MonitorChanges()
        {
            var thread = new Thread(new ThreadStart(StartMonitoringChanges));
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; FirePropertyChange(); }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; FirePropertyChange(); }
        }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
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
        
        public void Stop()
        {
            //player should never stop, it makes that "click" noise, whenever changing tone
            tonePlay.Stop();
        }

        ~BeatModel()
        {
            tonePlay.Dispose();            
        }
    }
}
