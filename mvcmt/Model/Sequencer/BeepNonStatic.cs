﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Threading;
using System.Diagnostics;

namespace Bit8Piano
{
    public class BeepNonStatic
    {
        //private SoundPlayer SP = new SoundPlayer();
        private SoundPlayer SP;
        private BinaryWriter BW;
        private MemoryStream memoryStream;

        private double deltaFT;

        public BeepNonStatic()
        {
            CreateSoundPlayer();

            var optimalLoopDuration = 1000;
            Samples = GetSamples(optimalLoopDuration);
        }

        private void CreateSoundPlayer()
        {
            SP = new SoundPlayer();
        }
        // > 100 throws exception
        private const short VOLUME_AMOUNT = 100;
        private double workingAmplitude;
        private int Samples;

        public void Play(double frequency, double duration)
        {
            BeepBeep(frequency);
        }

        public void Stop()
        {
            //BeepBeep(frequency, 1000 , true);
            SP.Stop();
        }

        private void BeepBeep(double Frequency, bool EndPart = false)
        {
            WriteToneToStream(Frequency, EndPart);

            SetStreamToTheBegining();

            System.IO.File.WriteAllBytes("tone.wav", memoryStream.ToArray());

            SetSoundPlayerStream();

            //SP.Stream.Seek(12, SeekOrigin.Current);

            PlaySoundPlayer();
            //Dispose();
        }

        private void SetSoundPlayerStream()
        {
            SP.Stream = memoryStream;
        }

        private void SetStreamToTheBegining()
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
        }

        private void PlaySoundPlayer()
        {
            SP.Play();
        }

        private double Amplitude
        {
            get
            {
                return ((VOLUME_AMOUNT * (System.Math.Pow(2, 15))) / 100) - 1;
            }
        }

        private double GetDeltaFT(double frequency)
        {
            var samplesPerSecond = 44100.0;
            var lenghtOfCurve = 2 * Math.PI;

            var deltaFT = lenghtOfCurve * frequency / samplesPerSecond;

            return deltaFT;
        }

        //values var is equivalent to Samples
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


        private void WriteToneToStream(double frequency, bool EndPart)
        {
            workingAmplitude = Amplitude;

            this.deltaFT = GetDeltaFT(frequency);

            int Bytes = PrepareStreamWithSamples(Samples);
            WriteWavHeaderToStream(Bytes);
          
            SamplesCollection samplesCollection = new SamplesCollection(Samples);
            IIterator iterator = samplesCollection.GetEnumerator();

            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!
             for (int T = iterator.FirstItem; iterator.IsDone == false; T = iterator.NextItem)
            {
                short Sample = GetSamples(Amplitude, this.deltaFT, iterator.CurrentIndex);

                WriteActualToneToWriter(Sample);
            }
            BW.Flush();
        }

        private short GetSamples(double amplitude, double deltaFT, int T)
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
            memoryStream = new MemoryStream(44 + Bytes);
            BW = new BinaryWriter(memoryStream);
            return Bytes;
        }
        
        private void WriteActualToneToWriter(double Sample)
        {
            BW.Write(Sample);
            BW.Write(Sample);
        }

        private void WriteWavHeaderToStream(int Bytes)
        {
            int[] wavHeader = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
            for (int I = 0; I < wavHeader.Length; I++)
            {
                BW.Write(wavHeader[I]);
            }
        }

        public void Dispose()
        {
            //SP.Stop();
            SP.Dispose();
            BW.Dispose();
        }
    }
}
