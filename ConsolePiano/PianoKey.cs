using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsolePiano.InstrumentalNote;

namespace ConsolePiano
{
    public interface IPianoKey
    {
        void Play();
        MusicalTone Tone { get; set; }
    }

    class PianoKey : IPianoKey
    {
        private MusicalTone tone;
        private Type type;

        public MusicalTone Tone { get { return tone; } set { tone = value; } }

        public enum Type { White, Black }

        public PianoKey(MusicalTone tone, Type type)
        {
            this.tone = tone;
            this.type = type;
        }

        public void Play()
        {
            var i = new CreateAndPlayInstrumentTone();
            i.Play((double)this.tone);

            Console.WriteLine("The key {0} sounds", this.tone);

            var delayWhileIstrumentPlays = 202;
            System.Threading.Thread.Sleep(delayWhileIstrumentPlays);
        }
    }
}
