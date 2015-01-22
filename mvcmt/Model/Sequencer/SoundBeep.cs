using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Threading;
using System.Diagnostics;
using Bit8Piano.Model.Sequencer.ADSR;
using NAudio.Wave;
using NAudio;

namespace Bit8Piano
{
    interface IModel
    {
        void Attach(IEventObserver observerView);
    }

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
            var optimalLoopDuration = 200;
            Samples = GetSamples(optimalLoopDuration);

        }

        private void CreateSoundPlayer()
        {
            SP = new SoundPlayer();
        }
        // > 100 throws exception
        private short VOLUME_AMOUNT = 30;
        private double workingAmplitude;
        private int Samples;
       
        private WaveOut waveOut;

        public void Play(double frequency, double duration)
        {
            BeepBeep(frequency);
        }

        public void Stop()
        {
            //most flexible would be to get current position wihin the stream'
            //and calcul. to the 0

            //OR  if current < endPhase move current to the final phase
            //else let the end phase fade out

            //if other tone is pressed -> transition, calculation of new sound between known tones?

            //var positionWhenStopped = waveOut.GetPosition();

            //if (positionWhenStopped < memoryStream.Length)
            //{
      
            //}
        }

       
        public void Attach(IEventObserver observerView)
        {
            //changed += observerView.HandleEvent;
        }


        private void BeepBeep(double Frequency)
        {
            WriteToneToStream(Frequency, this.memoryStream);
            SetStreamToTheBegining(this.memoryStream);

            //System.IO.File.WriteAllBytes("tone.wav", memoryStream.ToArray());

           
            // 'Create a Waveprovider,in this case a Stream called WaveStream 
            var waveStream = new WaveFileReader(this.memoryStream);
            // 'Now you can specify a position in your WaveStream.
            //'Alternatively use "WaveStream.seek(2000000,SeekOrigin.Begin)" here.
            //waveStream.Position = 0;


            //NAudio needs the COMPLETE WaveFile at this point including the header. 
            //DON´T set a position here!Use the Init function, to prepare playback.
            waveOut = new WaveOut();
            waveOut.Init(waveStream);
            waveOut.Play();
  
        }

        private void SetSoundPlayerStream()
        {
            
        }

        private void SetStreamToTheBegining(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
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

        #region Scheme
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
        #endregion


        private void WriteToneToStream(double frequency, MemoryStream stream)
        {
            workingAmplitude = Amplitude;

            this.deltaFT = GetDeltaFT(frequency);

            int Bytes = PrepareStreamWithSamples(stream, Samples);
            WriteWavHeaderToStream(Bytes);

            double minSound = 0.0;

            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!
            var realAttackDuration = PhaseDuration(AttackPhase.duration);
            var realDecayDuration = realAttackDuration + PhaseDuration(DecayPhase.duration);
            var realSustainDuration = realDecayDuration + PhaseDuration(SustainPhase.duration);
            var realReleaseDuration = realSustainDuration + PhaseDuration(SustainPhase.duration);

            for (int T = 0; T < Samples; T++)
            {
                if (T < realAttackDuration)
                {
                    minSound += AttackPhase.strength / PhaseDuration(AttackPhase.duration);
                }

                else if (T > realAttackDuration && T < realDecayDuration)
                {
                    minSound += -(AttackPhase.strength - DecayPhase.strength) / PhaseDuration((double)DecayPhase.duration);
                }

                else if (T > realDecayDuration && T < realSustainDuration)
                {
                    minSound += -(DecayPhase.strength - SustainPhase.strength) / PhaseDuration((double)SustainPhase.duration);
                }

                else if (T > realSustainDuration && T < realReleaseDuration && minSound > 0)
                {
                    minSound += -(SustainPhase.strength - ReleasePhase.strength) / PhaseDuration((double)ReleasePhase.duration);
                }
                
                var Sample = GetFinalSamples(minSound, this.deltaFT, T);
                WriteActualToneToWriter(Sample);
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

        private int PrepareStreamWithSamples(MemoryStream stream, int Samples)
        {
            int Bytes = Samples * sizeof(int);
            stream = new MemoryStream(44 + Bytes);
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

        public void Dispose()
        {
            SP.Dispose();
            //BW.Dispose();
        }
    }
}
