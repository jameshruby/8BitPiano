using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using System.IO;

namespace ConsolePiano.InstrumentalNote
{
    class TonePlayer
    {
        public static void Play(MemoryStream streamToPlay)
        {
            // Create a Waveprovider,in this case a Stream called WaveStream 
            var waveStream = new WaveFileReader(streamToPlay);
            //NAudio needs the COMPLETE WaveFile at this point including the header. 
            //DON´T set a position here!Use the Init function, to prepare playback.
            WaveOut waveOut = new WaveOut();
            waveOut.Init(waveStream);
            waveOut.Play();
        }
    }
}
