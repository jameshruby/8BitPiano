using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit8Piano
{
    interface IBeatModel
    {
        void PlayTone(Tone actualTone);

        void MonitorChanges();

        string FullName { get; }

        event Action OnPropertyChange;

        void Stop();

        void Attach(IEventObserver observerView);
    }
}
