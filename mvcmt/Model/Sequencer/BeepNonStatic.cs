using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Threading;
using System.Diagnostics;
using Bit8Piano.Model.Sequencer.ADSR;

namespace Bit8Piano
{
    public class BeepNonStatic
    {
        //private SoundPlayer SP = new SoundPlayer();
        private SoundPlayer SP;
        private BinaryWriter BW;
        private MemoryStream memoryStream;
        private List<byte> byteArray = new List<byte>();

        private double deltaFT;

        public BeepNonStatic()
        {
            CreateSoundPlayer();

            var optimalLoopDuration = 1800;
            Samples = GetSamples(optimalLoopDuration);
        }

        private void CreateSoundPlayer()
        {
            SP = new SoundPlayer();
        }
        // > 100 throws exception
        private short VOLUME_AMOUNT = 100;
        private double workingAmplitude;
        private int Samples;
        private double tempStrength;

        public void Play(double frequency, double duration)
        {
            BeepBeep(frequency);
        }

        public void Stop()
        {
            //BeepBeep(frequency, 1000 , true);

            //SP.Stream.Length - (SP.Stream.Length / 2
            //SP.Stream.Seek(0, SeekOrigin.Current);
            //SP.Stream.Position = SP.Stream.Length;

            SP.Stop();


            //var currentPlayed = SP.Stream.Position;
            //if (currentPlayed != SP.Stream.Length)
            //{
              //  SP.Stream.Position = SP.Stream.Length-5;
                //SP.Stream.Seek(SP.Stream.Length - 3, SeekOrigin.Begin);
                //SP.Load();
                
            //}

            //memoryStream.Position = SP.Stream.Length - 5;
            //SP.Stream = memoryStream;
            //SP.Play();


            //SP.Stream.Seek(12, SeekOrigin.);
          //if(SP.Stream.Position == SP.Stream.Length - 1)  
            //
        }

        private void BeepBeep(double Frequency, bool EndPart = false)
        {
            WriteToneToStream(Frequency, EndPart);

            SetStreamToTheBegining();

            //System.IO.File.WriteAllBytes("tone.wav", memoryStream.ToArray());

            SetSoundPlayerStream();

            //SP.SoundLocation = "tone.wav";

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

        private void WriteActualToneToWriter(short Sample)
        {
            BW.Write(Sample);
            BW.Write(Sample);
        }

        private void WriteToneToStream(double frequency, bool EndPart)
        {
            workingAmplitude = Amplitude;

            this.deltaFT = GetDeltaFT(frequency);

            int Bytes = PrepareStreamWithSamples(Samples);
            WriteWavHeaderToStream(Bytes);


            const double duration = (double)1 / 2;
            const double strength = 32767.0;



            double minSound = 0;
            double tempStrength = 0.0;
            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!

            double cyclesDecrement = strength / (Samples/2);
            for (int T = 0; T < Samples; T++)
            {
                //if (T < Samples / 2)
                //    minSound += cyclesDecrement;
                //else
                //    minSound -= cyclesDecrement;


                minSound = Amplitude;

                //if (minSound < 100)
                //{
                //    VOLUME_AMOUNT = (short)minSound;
                 
                //}
                //else
                //{
                //    minSound = 0;
                //}

                if (T < GetSamplesDuration(AttackPhase.duration))
                {
                    minSound += AttackPhase.strength / GetSamplesDuration(AttackPhase.duration);
                    tempStrength = T;

                }

                //else if (T < (tempStrength + GetSamplesDuration(DecayPhase.duration)))
                //{
                //    minSound += -(AttackPhase.strength - DecayPhase.strength) / GetSamplesDuration((double)DecayPhase.duration);
                //    tempStrength = T;
                //}

                //else if (T < (tempStrength + GetSamplesDuration(SustainPhase.duration)))
                //{
                //    minSound += -((double)DecayPhase.duration - SustainPhase.strength) / GetSamplesDuration((double)SustainPhase.duration);
                //     tempStrength = T;
                //}
                //else if (T < (tempStrength + GetSamplesDuration((double)ReleasePhase.duration)))
                //{
                //    minSound += -((double)SustainPhase.duration - ReleasePhase.strength) / GetSamplesDuration((double)ReleasePhase.duration);
                //     tempStrength = T;
                //}

                //else if (T < GetSamplesDuration((double)InstrumentDuration.Sustain))
                //{
                //    minSound += ((double)InstrumentDuration.Decay - (double)InstrumentStrength.Sustain) / GetSamplesDuration((double)InstrumentDuration.Sustain);
                //}

                //else if (T < GetSamplesDuration((double)InstrumentDuration.Decay))
                //{
                //    minSound += ((double)InstrumentDuration.Sustain - (double)InstrumentStrength.Release) / GetSamplesDuration((double)InstrumentDuration.Release);
                //}



                short Sample = System.Convert.ToInt16(minSound * Math.Sin(this.deltaFT * T));
                BW.Write(Sample);
                BW.Write(Sample);
              
            }

            BW.Flush();
        }

        private double GetSamplesDuration(double duration)
        {
            return Samples * duration;
        }

        public static short GetSamples(double amplitude, double deltaFT, double T)
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
            //BW.Dispose();
        }
    }
}
