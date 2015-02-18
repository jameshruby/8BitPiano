using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano
{
    class Program
    {
        static void Main(string[] args)
        {
            var piano = new Piano();
            
            piano.AddKey(new PianoKey(MusicalTone.C, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.D, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.E, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.F, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.G, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.A, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.B, PianoKey.Type.White));
            piano.AddKey(new PianoKey(MusicalTone.C5, PianoKey.Type.White));
                
            piano.PlayKey(MusicalTone.C);
            piano.PlayKey(MusicalTone.D);
            piano.PlayKey(MusicalTone.E);
            piano.PlayKey(MusicalTone.F);
            piano.PlayKey(MusicalTone.G);
            piano.PlayKey(MusicalTone.G);
            piano.PlayKey(MusicalTone.B);
            piano.PlayKey(MusicalTone.A);
            piano.PlayKey(MusicalTone.G);
            piano.PlayKey(MusicalTone.F);

            Console.ReadLine();
        }
    }
}
