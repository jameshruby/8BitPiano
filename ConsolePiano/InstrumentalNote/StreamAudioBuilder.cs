using System;
using System.Collections.Generic;
using System.IO;

namespace ConsolePiano.InstrumentalNote
{
    public class StreamAudioBuilder
    {
        private MemoryStream memoryStream;
        private List<double> preparedInstrumentADSRTone = new List<double>();
      
        public StreamAudioBuilder(int toneDuration)
        {
            var samplesSize = GetSampleSize(toneDuration);
            WriteADSRPhaseToStream(samplesSize);
        }
        /// <summary>
        /// Scheme : //values var is equivalent to Samples
        // attack   decay sustain  release
        //---------   ----  ----- -----> t
        //           -
        //         -   -
        //       -      -
        //     -          ------ 
        //   -                   -
        // -                       - 
         /// </summary>
        /// <param name="frequency"></param>
        
        private void WriteADSRPhaseToStream(int samplesSize)
        {
            var instrumentNote = new DefaultInstrumentNote();

            for (int T = 0; T < samplesSize; T++)
            {
                instrumentNote.ToNextNote(T);

                if (instrumentNote.Phase is EndPhase)
                {
                    var Sample = 0.0;

                    this.preparedInstrumentADSRTone.Add(Sample);
                }
                else
                {
                    var Sample = instrumentNote.CurrentNote;
                    this.preparedInstrumentADSRTone.Add(Sample);
                }
            }
        }

        private static int GetSampleSize(double Duration)
        {
            var sampleRate = 441.0;
            return (int)(sampleRate * Duration / 10);
        }

        public MemoryStream GenerateTone(double Frequency)
          {
            int Bytes = GetBytesForHeader(this.preparedInstrumentADSRTone.Count);
            int[] headerWav = GetWavHeader(Bytes);
            this.memoryStream = CreateStream(Bytes);

            BinaryWriter BW = new BinaryWriter(this.memoryStream);

            WriteWavHead(headerWav, BW);

            double deltaFT = GetDeltaFT(Frequency);

            for (int T = 0; T < this.preparedInstrumentADSRTone.Count; T++)
            {
                var freqTime = Math.Sin(deltaFT * T);
                var finalNote = freqTime * this.preparedInstrumentADSRTone[T];
                WriteActualToneToWriter(System.Convert.ToInt16(finalNote), BW);
            }

            BW.Flush();
        
            SetStreamToTheBegining(this.memoryStream);
#if Debug
		  System.IO.File.WriteAllBytes("lastTone.wav", MS.ToArray());
#endif
            return this.memoryStream;
        }

          private MemoryStream CreateStream(int Bytes)
          {
              return new MemoryStream(44 + Bytes);
          }

        private static int[] GetWavHeader(int Bytes)
        {
            int[] headerWav = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
            return headerWav;
        }

        private static int GetBytesForHeader(int samplesSize)
        {
            int Bytes = samplesSize * sizeof(int);
            return Bytes;
        }

        private static void WriteWavHead(int[] headerWav, BinaryWriter BW)
        {
            for (int I = 0; I < headerWav.Length; I++)
            {
                BW.Write(headerWav[I]);
            }
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

        private void WriteActualToneToWriter(short Sample, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(Sample);
            binaryWriter.Write(Sample);
        }

        private static short GetFinalSamples(double amplitude, double deltaFT, double T)
        {
            var freqTime = Math.Sin(deltaFT * T);
            var totalnote = amplitude * freqTime;

            return System.Convert.ToInt16(totalnote);
        }
    }
}

