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
        private int Samples;

        private List<double> preparedInstrumentADSRTone = new List<double>();

        public CreateAndPlayInstrumentTone()
        {
            //GenerateADSRPhases
            var optimalLoopDuration = 200;
            Samples = GetSamples(optimalLoopDuration);

            WriteOnlyADSRPhaseToStream();
        }

        public void Play(double frequency)
        {
            GenerateToneAndPlayIt(frequency);
        }
        private void GenerateToneAndPlayIt(double Frequency)
        {
            WriteFrequencyWithPreparedStream(Frequency);
            SetStreamToTheBegining(this.memoryStream);

            //System.IO.File.WriteAllBytes("tone.wav", memoryStream.ToArray());

            // Create a Waveprovider,in this case a Stream called WaveStream 
            var waveStream = new WaveFileReader(this.memoryStream);
            //NAudio needs the COMPLETE WaveFile at this point including the header. 
            //DON´T set a position here!Use the Init function, to prepare playback.
            WaveOut waveOut = new WaveOut();
            waveOut.Init(waveStream);
            waveOut.Play();
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
        private void WriteOnlyADSRPhaseToStream()
        {
            //int Bytes = PrepareStreamWithSamples(Samples);
            //WriteWavHeaderToStream(Bytes);

            var instrumentNote = new DefaultInstrumentNote();

            //var optimalLoopDuration = 200;
            //Samples = GetSamples(optimalLoopDuration);

            for (int T = 0; T < Samples; T++)
            {
                instrumentNote.ToNextNote(T);

                if (instrumentNote.State is EndState)
                {
                    var Sample = 0.0; //GetFinalSamples(0.0, this.deltaFT, T);

                    this.preparedInstrumentADSRTone.Add(Sample);
                }
                else
                {
                    var Sample = instrumentNote.CurrentNote;// GetFinalSamples(instrumentNote.CurrentNote, this.deltaFT, T);
                    this.preparedInstrumentADSRTone.Add(Sample);
                }
            }
        }


        private void WriteFrequencyWithPreparedStream(double frequency)
        {
            double deltaFT = GetDeltaFT(frequency);
            //wav builder
            int Bytes = PrepareStreamWithSamples(Samples);
            WriteWavHeaderToStream(Bytes);
          
            for (int T = 0; T < this.preparedInstrumentADSRTone.Count; T++)//foreach (var tone in this.preparedInstrumentADSRTone) // 
            {
                var freqTime = Math.Sin(deltaFT * T);
                var finalNote = freqTime * this.preparedInstrumentADSRTone[T];
                WriteActualToneToWriter(System.Convert.ToInt16(finalNote));
            }
            BW.Flush();
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

        private void WriteActualToneToWriter(short Sample)
        {
            BW.Write(Sample);
            BW.Write(Sample);
        }

        public static short GetFinalSamples(double amplitude, double deltaFT, double T)
        {
            var freqTime = Math.Sin(deltaFT * T);
            var totalnote = amplitude * freqTime;

            return System.Convert.ToInt16(totalnote);
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

