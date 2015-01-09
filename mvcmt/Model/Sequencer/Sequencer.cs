using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bit8Piano
{
    public class PianoModel
    {
        private Thread sequencerThread;
        private bool isrunning;

        private int durationWithTempo;
        private object locker = new Object();

 //int realNoteDuration = (ushort)noteDuration / (int)AudioTape.DanceTempo.Polka;
        
        public static void Stop()
        {
            BeepThreading.Stop();
        }

        public static void Play(Tone note, ushort realNoteDuration)
        {
            if (note == Tone.SILENCE)
            {
                Thread.Sleep(realNoteDuration);
            }
            else
            {
                //BeepNonStatic.Play((double)note, realNoteDuration);
            }
        }

        ~PianoModel()
        {
            //BeepNonStatic.Dispose();
        }
    }
}
