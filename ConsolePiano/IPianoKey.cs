using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano
{
    public interface IPianoKey
    {
        MusicalTone Tone { get; set; }
        string Alias { get; set; }

        void Play(ConsolePiano.InstrumentalNote.StreamAudioBuilder i);
    }
}
