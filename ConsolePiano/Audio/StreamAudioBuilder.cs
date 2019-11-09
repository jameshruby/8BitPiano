using System;
using System.Collections.Generic;
using System.IO;

namespace ConsolePiano.InstrumentalNote
{
    public class StreamAudioBuilder
    {
        private const int sampleRate = 44100;
        private const int bitDepth = 16;
        //only Singleton so far
        private static StreamAudioBuilder streamAudioBuilderSingleton;
        public static StreamAudioBuilder GetInstance()
        {
            return streamAudioBuilderSingleton ?? (streamAudioBuilderSingleton = new StreamAudioBuilder());
        }
        private StreamAudioBuilder() { }
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
            return (int)(sampleRate * Duration / 1000.0);
        }
        public MemoryStream GenerateTone(List<double> preparedTone, double frequency)
        {
            //TODO DEBUG envelope values
            File.Delete("debug.txt");
            using (StreamWriter streamwriter = new StreamWriter("debug.txt", true, System.Text.Encoding.UTF8))
            { preparedTone.ForEach(x => streamwriter.WriteLine(x.ToString())); }

            Int16[] rawAudioStream = GenerateRawAudioSequence(preparedTone, frequency);
            MemoryStream memoryStream = GenerateAudioStream(rawAudioStream);
            //DEBUG
            System.IO.File.WriteAllBytes("lastTone.wav", memoryStream.ToArray());
            return memoryStream;
        }
        private Int16[] GenerateRawAudioSequence(List<double> preparedTone, double frequency)
        {
            double deltaFT = GetDeltaFT(frequency);
            var rawAudioStream = new Int16[preparedTone.Count];
            for (int T = 0; T < preparedTone.Count; T++)
                rawAudioStream[T] = GetFinalSamples(preparedTone[T], deltaFT, T);
            return rawAudioStream;
        }

        private MemoryStream GenerateAudioStream(Int16[] rawAudioStream)
        {
            int size = GetSize(rawAudioStream.Length);
            MemoryStream memoryStream = CreateStream(size);
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);

            int[] headerWav = GetHeader(size);
            WriteHeader(headerWav, binaryWriter);

            foreach (var convertedNote in rawAudioStream)
                WriteActualToneToWriter(convertedNote, binaryWriter);

            binaryWriter.Flush();
            SetStreamToTheBegining(memoryStream);
            return memoryStream;
        }
        /// AUDIO STREAM
        private double GetDeltaFT(double frequency)
        {
            var lenghtOfCurve = 2 * Math.PI;
            var deltaFT = lenghtOfCurve * frequency / sampleRate;
            return deltaFT;
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
        //WAV
        private static int GetSize(int samplesSize)
        {
            return samplesSize * sizeof(int);
        }
        private MemoryStream CreateStream(int size)
        {
            return new MemoryStream(44 + size);
        }
        private static int[] GetHeader(int size)
        {
            int[] headerWav = { 0X46464952, 36 + size, 0X45564157, 0X20746D66, bitDepth, 0X20001, sampleRate, 176400, 0X100004, 0X61746164, size };
            return headerWav;
        }
        private static void WriteHeader(int[] headerWav, BinaryWriter binaryWriter)
        {
            for (int I = 0; I < headerWav.Length; I++)
            {
                binaryWriter.Write(headerWav[I]);
            }
        }
        private void WriteActualToneToWriter(short Sample, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(Sample);
            binaryWriter.Write(Sample);
        }
        private void SetStreamToTheBegining(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
        }
    }
}

