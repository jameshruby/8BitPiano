using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bit8Piano
{
    static class Program
    {
        /// <summary>
        ///      
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IBeatModel beatModel = new BeatModel();
            IBeatController controller = new BeatController(beatModel);

            Application.Run(controller.View);

            // i can easily switch to heart mode

            //HeartModel heartModel = new HeartModel();
            //IBeatController controller = new HeartController(heartModel);

            //Application.EnableVisualStyles();
            //Application.Run(new TestAwaitUI());   /
        }
    }
}
