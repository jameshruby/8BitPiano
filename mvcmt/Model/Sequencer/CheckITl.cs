using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bit8Piano
{
    class CheckIt 
    {
        private static readonly string[] firstNames = new[] { "Jane", "Jack", "Joe" };
        private static readonly string[] lastNames = new[] { "Doe", "Black", "White" };
        private static readonly Random rand = new Random();

        private const int DELAY = 1000;
        private string lastName;
        private string firstName;

        public event Action OnPropertyChange;
        private BeepNonStatic tonePlay;
        private System.IO.Stream stream;


        //static void Main()
        //{
        //    Thread t = new Thread(Print);
        //    t.Start("Hello from t!");
        //}

        //static void Print(object messageObj)
        //{
        //    string message = (string)messageObj;   // We need to cast here
        //    Console.WriteLine(message);
        //}


        public CheckIt(System.IO.Stream stream)
        {
            // TODO: Complete member initialization
            this.stream = stream;

            var thread = new Thread(new ParameterizedThreadStart(StartMonitoringChanges));
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start(stream);



            //var thread = new Thread(new ThreadStart(StartMonitoringChanges(stream)));
            //thread.IsBackground = true;
            //thread.Priority = ThreadPriority.Normal;
            //thread.Start();
        }

        //public void CheckIt()
        //{
        //    //var thread = new Thread(new ThreadStart(StartMonitoringChanges(stream)));
        //    //thread.IsBackground = true;
        //    //thread.Priority = ThreadPriority.Normal;
        //    //thread.Start();
        //}


        private void StartMonitoringChanges(object stream)
        {
            var v = (System.IO.MemoryStream)stream;


            while (true)
            {
                //if(stream.)


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
    }
}
