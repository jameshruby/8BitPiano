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
    public class BeepNonStaticCopy
    {
        //private SoundPlayer SP = new SoundPlayer();
        private SoundPlayer SP;
        private BinaryWriter BW;
        private MemoryStream memoryStream;
        private List<byte> byteArray = new List<byte>();

        private double deltaFT;

        public BeepNonStaticCopy()
        {
            CreateSoundPlayer();

            var optimalLoopDuration = 3000;
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
        private double tempStrength;

        public void Play(double frequency, double duration)
        {
            BeepBeep(frequency);
        }

        public void Stop()
        {
            //BeepBeep(frequency, 1000 , true);
            
            //SP.Stream.Length - (SP.Stream.Length / 2
            SP.Stream.Seek(0, SeekOrigin.Current);
            
            
            
            //var f = SP.Stream.Position;
            //SP.Play();
          

            //SP.Stream.Seek(12, SeekOrigin.);
            
            //SP.Stop();
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
            double attackDuration = Samples / 20;
            double decayDuration = Samples / 9;
            double sustainDuration = Samples / 4;
            double releaseDuration = Samples / 6;

            workingAmplitude = Amplitude;

            this.deltaFT = GetDeltaFT(frequency);

            int Bytes = PrepareStreamWithSamples(Samples);
            WriteWavHeaderToStream(Bytes);

            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!
            for (int T = 0; T < Samples; T++)
            {
                short Sample = 15000;
                //short Sample = System.Convert.ToInt16(Amplitude * Math.Sin(this.deltaFT * T));
                BW.Write(Sample);
                BW.Write(Sample);
            }

            //for (int T = iterator.FirstItem; iterator.IsDone == false; T = iterator.NextItem)
            //potrebuju jet spis vysledek iterace a zapamovatoat si vysledek rpeovcbi

            //var attackResult = AttackState.Iterate();
            //var decayResult = DecayState.Iterate(attackResult);
            //var sustainResult = SustainState.Iterate(decayResult);
            //var releaseResult = ReleaseState.Iterate(sustainResult);

            //double at = PianoAttack();
            //double dc = PianoDecay(at);
           
            //double su = PianoSustain(dc);
            
            //double rs = PianoRelease(su);

            BW.Flush();
        }

        public double PianoAttack()
        {
            const double duration = (double)1 / 20;
            const double strength = 32767.0;

            double samplesDuration = Samples; // - 15000  * duration;

            double cyclesDecrement = strength / samplesDuration;

            double T;
            double minSound = 0.0;
            for (T = 0; T < samplesDuration; T++)
            {
                //if (T < samplesDuration / 2)
                    minSound += cyclesDecrement;
                //else
                //    minSound -= cyclesDecrement;

                short Sample = GetSamples(minSound, this.deltaFT, T);

                byte[] tempByteArray = BitConverter.GetBytes(Sample);

                byteArray.AddRange(tempByteArray);
                byteArray.AddRange(tempByteArray);

                //WriteActualToneToWriter(Sample);
            }
           
            //BW.Write(byteArray.ToArray());
            
            
            this.tempStrength = minSound;
            return T;
        }

     
        public double PianoDecay(double attackPosition)
        {
            const double duration = (double)1 / 3;
            const double strength = 18000.0;

             double samplesDuration = Samples * duration;

            #region MyRegion
            //double attackFinalStrength = amplitude;//32767.0;
            //double decayFinalStrength = 18000.0;
            /*
            
            double attackDuration = Samples / 20;
            double decayDuration = Samples / 9;
            double sustainDuration = Samples / 4;
            double releaseDuration = Samples / 6
        
            double attackPhase = attackDuration;
            double decayPhase = attackDuration + decayDuration;
            double sustainPhase = decayPhase + sustainDuration;
            double releasePhase = sustainPhase + releaseDuration;
            
            cyclesCount = decayPhase - attackDuration;
            cyclesDecrement = (attackFinalStrength - decayFinalStrength) / cyclesCount;

            TraverseSamplesFromToAndAddToneToStream(attackPhase, decayPhase, attackFinalStrength, -cyclesDecrement);

            cyclesCount = decayPhase - attackDuration;
            cyclesDecrement = (attackFinalStrength - decayFinalStrength) / cyclesCount;
             */

            #endregion

            //(this.tempStrength - strength) / samplesDuration

            //double cyclesDecrement = Amplitude / samplesDuration;
            double cyclesDecrement = (this.tempStrength - strength) / samplesDuration;

            //    iterator.IteratorStep = -(int)cyclesDecrement;
            double T;
            double minSound = this.tempStrength;
            for (T = 0; T < samplesDuration; T++)
            {
                minSound += -cyclesDecrement;

                short Sample = GetSamples(Amplitude, this.deltaFT, T);
                //WriteActualToneToWriter(Sample);

                byte[] tempByteArray = BitConverter.GetBytes(Sample);

                byteArray.AddRange(tempByteArray);
                byteArray.AddRange(tempByteArray);
            }
           
             BW.Write(byteArray.ToArray());
            
            this.tempStrength = minSound;
            return T;
        }
        public double PianoSustain(double decayPosition)
        {
            const double duration = (double)1 / 2;
            const double strength = 14000.0;

            double samplesDuration = Samples * duration;
            
            var cyclesDecrement = (this.tempStrength - strength) / samplesDuration;

            double T;
            double minSound = this.tempStrength;
            for (T = 0; T < samplesDuration; T++)
            {
                minSound += -cyclesDecrement;

                short Sample = GetSamples(minSound, this.deltaFT, T);
                WriteActualToneToWriter(Sample);
            }
            this.tempStrength = minSound;
            return T;
        }

        public double PianoRelease(double sustainPosition)
        {
            const double duration = (double)1 / 2;
            const double strength = 0.0;

            double samplesDuration = Samples * duration;

            var cyclesDecrement = (this.tempStrength - strength) / samplesDuration;

            double T;
            double minSound = this.tempStrength;
            for (T = 0; T <= samplesDuration; T++)
            {
                minSound -= cyclesDecrement;

                short Sample = GetSamples(minSound, this.deltaFT, T);
                WriteActualToneToWriter(Sample);
            }

            return T;
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
            BW.Dispose();
        }
    }
}
