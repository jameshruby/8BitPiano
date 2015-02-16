using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsolePiano.InstrumentalNote;

namespace ConsolePiano
{
    public class PianoKey : IPianoKey
    {
        private MusicalTone tone;
        private Type type;
        private string alias;

        public MusicalTone Tone
        {
            get { return tone; }
            set { tone = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public enum Type
        {
            White, Black
        }

        public PianoKey(MusicalTone tone, Type type, string alias)
        {
            this.tone = tone;
            this.type = type;
            this.alias = alias;
        }

        public void Play(StreamAudioBuilder i)
        {
            var tone = i.GenerateTone((double)this.tone);
            TonePlayer.Play(tone);

            Console.WriteLine("The key {0} sounds", this.tone);

            var delayWhileIstrumentPlays = 202;
            System.Threading.Thread.Sleep(delayWhileIstrumentPlays);
        }

    }
}
