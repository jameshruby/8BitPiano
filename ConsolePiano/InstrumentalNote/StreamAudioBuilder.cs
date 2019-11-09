using System;
using System.Collections.Generic;
using System.IO;

namespace ConsolePiano.InstrumentalNote
{
    public class StreamAudioBuilder
    {
        //only Singleton so far
        private static StreamAudioBuilder streamAudioBuilderSingleton;
        public static StreamAudioBuilder GetInstance()
        {
            return streamAudioBuilderSingleton ?? (streamAudioBuilderSingleton = new StreamAudioBuilder());
        }
        private StreamAudioBuilder() {}

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
        public int GetSampleSize(double Duration)
        {
            var sampleRate = 441.0;
            return (int)(sampleRate * Duration / 10.0);
        }

        public MemoryStream GenerateTone(List<double> preparedTone, double frequency)
        {
            int Bytes = GetBytesForHeader(preparedTone.Count);
            int[] headerWav = GetWavHeader(Bytes);
            MemoryStream memoryStream = CreateStream(Bytes);

            BinaryWriter BW = new BinaryWriter(memoryStream);

            WriteWavHead(headerWav, BW);

            double deltaFT = GetDeltaFT(frequency);

            File.Delete("debug.txt");
            using (StreamWriter streamwriter = new StreamWriter("debug.txt", true, System.Text.Encoding.UTF8))
            {
                for (int T = 0; T < preparedTone.Count; T++)
                {
                    streamwriter.WriteLine(preparedTone[T].ToString());
                    Int16 convertedNote = GetFinalSamples(preparedTone[T], deltaFT, T);
                    WriteActualToneToWriter(convertedNote, BW);
                }
            }
            BW.Flush();

            SetStreamToTheBegining(memoryStream);

            System.IO.File.WriteAllBytes("lastTone.wav", memoryStream.ToArray());
#if Debug
		  //System.IO.File.WriteAllBytes("lastTone.wav", MS.ToArray());
#endif
            return memoryStream;
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
            Int16 convertedNote;
            try { convertedNote = System.Convert.ToInt16(totalnote); }
            catch (OverflowException ex) { throw new Exception("Note value out of range( " + totalnote + " )", ex); }
            return System.Convert.ToInt16(totalnote);
        }
    }
}

