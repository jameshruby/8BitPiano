using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Threading;
using System.Diagnostics;

using NAudio.Wave;
using NAudio;

namespace ConsolePiano.InstrumentalNote
{
    public class CreateAndPlayInstrumentTone
    {
        private BinaryWriter BW;
        private MemoryStream memoryStream;
    
        private List<byte> byteArray = new List<byte>();

        private double deltaFT;

        private int Samples;

        private WaveOut waveOut;

        public void Play(double frequency)
        {
            GenerateToneADSRPhasesAndPlayIt(frequency);
        }

        private void GenerateToneADSRPhasesAndPlayIt(double Frequency)
        {
            var optimalLoopDuration = 200;
            Samples = GetSamples(optimalLoopDuration);

            WriteFrequencyADSRPhaseToStream(Frequency);

            SetStreamToTheBegining(this.memoryStream);

            //System.IO.File.WriteAllBytes("tone.wav", memoryStream.ToArray());

            // Create a Waveprovider,in this case a Stream called WaveStream 
            var waveStream = new WaveFileReader(this.memoryStream);

            //NAudio needs the COMPLETE WaveFile at this point including the header. 
            //DON´T set a position here!Use the Init function, to prepare playback.
            waveOut = new WaveOut();
            waveOut.Init(waveStream);
            waveOut.Play();
        }

        private void SetStreamToTheBegining(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
        }

        private double GetDeltaFT(double frequency)
        {
            var samplesPerSecond = 44100.0;
            var lenghtOfCurve = 2 * Math.PI;

            var deltaFT = lenghtOfCurve * frequency / samplesPerSecond;

            return deltaFT;
        }

        /// <summary>
        /// Scheme : //values var is equivalent to Samples
        //end of every phase is defined by CurrentItem in iterator
        //
        // attack   decay sustain  release
        //---------   ----  ----- -----> t
        //           -
        //         -   -
        //       -      -
        //     -          ------ 
        //   -                   -
        // -                       - 

        //... so with iterator pattern i dont need phase vars 
        /// </summary>
        /// <param name="frequency"></param>
        private void WriteFrequencyADSRPhaseToStream(double frequency)
        {
            this.deltaFT = GetDeltaFT(frequency);

            int Bytes = PrepareStreamWithSamples(Samples);
            WriteWavHeaderToStream(Bytes);

            var instrumentNote = new DefaultInstrumentNote();

            for (int T = 0; T < Samples; T++)
            {
                instrumentNote.ToNextNote(T);

                if (instrumentNote.State is EndState)
                {
                    var Sample = GetFinalSamples(0.0, this.deltaFT, T);
                    WriteActualToneToWriter(Sample);
                }
                else
                {
                    var Sample = GetFinalSamples(instrumentNote.CurrentNote, this.deltaFT, T);
                    WriteActualToneToWriter(Sample);
                }
            }
            BW.Flush();
        }
        private void WriteActualToneToWriter(short Sample)
        {
            BW.Write(Sample);
            BW.Write(Sample);
        }

        private double PhaseDuration(double duration)
        {
            return Samples * duration;
        }

        public static short GetFinalSamples(double amplitude, double deltaFT, double T)
        {
            return System.Convert.ToInt16(amplitude * Math.Sin(deltaFT * T));
        }

        private static int GetSamples(double Duration)
        {
            return (int)(441.0 * Duration / 10.0);
        }

        private int PrepareStreamWithSamples(int Samples)
        {
            int Bytes = Samples * sizeof(int);
            MemoryStream stream = new MemoryStream(44 + Bytes);
            BW = new BinaryWriter(stream);
            this.memoryStream = stream;

            return Bytes;
        }

        private void WriteWavHeaderToStream(int Bytes)
        {
            int[] wavHeader = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
            for (int I = 0; I < wavHeader.Length; I++)
            {
                BW.Write(wavHeader[I]);
            }
        }

    }
}
