using System;
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

        private List<byte> byteArray = new List<byte>();

        private double deltaFT;

        public BeepNonStatic()
        {
            CreateSoundPlayer();
            //Play(0, 50);
            //PlaySoundPlayer();
        }

        private void CreateSoundPlayer()
        {
            SP = new SoundPlayer();
        }
        // > 100 throws exception
        private const short VOLUME_AMOUNT = 100;
        private double workingAmplitude;

        public void Play(double frequency, double duration)
        {
            var optimalLoopDuration = 1000;
            BeepBeep(frequency, optimalLoopDuration);
        }

        public void Stop()
        {
            //BeepBeep(frequency, 1000 , true);
            SP.Stop();
        }

        private void BeepBeep(double Frequency, double Duration, bool EndPart = false)
        {
            WriteToneToStream(Frequency, Duration, EndPart);

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


        private void WriteToneToStream(double frequency, double Duration, bool EndPart)
        {
            var amplitude = Amplitude;

            workingAmplitude = amplitude;

            this.deltaFT = GetDeltaFT(frequency);

            int Samples = GetSamples(Duration);

            int Bytes = PrepareStreamWithSamples(Samples);

            WriteWavHeaderToStream(Bytes);

            //now going for first shot, considering max amplitude to be 32767
            //strength should be relative to size of amplitude which is relative from volume_level

            double attackFinalStrength = amplitude;//32767.0;
            double decayFinalStrength = 18000.0;
            double sustainFinalStrength = 14000.0;
            double releaseFinalStrength = 0.0;

            double attackDuration = Samples / 20;
            double decayDuration = Samples / 9;
            double sustainDuration = Samples / 4;
            double releaseDuration = Samples / 6;

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

            double attackPhase = attackDuration;
            double decayPhase = attackDuration + decayDuration;
            double sustainPhase = decayPhase + sustainDuration;
            double releasePhase = sustainPhase + releaseDuration;

            var currentT = 0.0;
            var samplesStart = 0.0;
            var cyclesCount = attackPhase - (currentT);
            var cyclesDecrement = Amplitude / attackDuration;

            // IIterator iterator =
            
            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!
            for (int T = 0; T < Samples; T++)
            {
                short Sample = GetSamples(amplitude, T);
                
                byte[] tempByteArray = BitConverter.GetBytes(Sample);

                byteArray.AddRange(tempByteArray);
                byteArray.AddRange(tempByteArray);
            }

            BW.Write(byteArray.ToArray());

            
            TraverseSamplesFromToAndAddToneToStream(samplesStart, attackPhase, 0.0, cyclesDecrement);
            //for next phase, all i need is value of T(which is same as nextPhase)

            cyclesCount = decayPhase - attackDuration;
            cyclesDecrement = (attackFinalStrength - decayFinalStrength) / cyclesCount;

            TraverseSamplesFromToAndAddToneToStream(attackPhase, decayPhase, attackFinalStrength, -cyclesDecrement);


            cyclesCount = sustainPhase - decayPhase;
            cyclesDecrement = (decayFinalStrength - sustainFinalStrength) / cyclesCount;

            TraverseSamplesFromToAndAddToneToStream(decayPhase, sustainPhase, decayFinalStrength, -cyclesDecrement);



            cyclesCount = releasePhase - sustainPhase;
            cyclesDecrement = (sustainFinalStrength - releaseFinalStrength) / cyclesCount;

            TraverseSamplesFromToAndAddToneToStream(sustainPhase, releasePhase, sustainFinalStrength, -cyclesDecrement);
            
            BW.Flush();
        }

        private short GetSamples(double amplitude, int T)
        {
            return System.Convert.ToInt16(amplitude * Math.Sin(this.deltaFT * T));
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

        private void TraverseSamplesFromToAndAddToneToStream(double samplesStart, double samplesEnd, double minSound, double amplitudeChangeSteps)
        {
            for (double T = samplesStart; T < samplesEnd; ++T)
            {
                minSound += amplitudeChangeSteps;
            
                workingAmplitude = Math.Round(minSound, 2);

                WriteActualToneToWriter(workingAmplitude, T);
            }
        }

        private void WriteActualToneToWriter(double amplitude, double T)
        {
            short Sample = System.Convert.ToInt16(amplitude * Math.Sin(this.deltaFT * T));
            
            //BW.Write(Sample);
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
