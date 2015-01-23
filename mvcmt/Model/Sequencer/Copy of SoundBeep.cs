//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Media;
//using System.Threading;
//using System.Diagnostics;
//using Bit8Piano.Model.Sequencer.ADSR;

//namespace Bit8Piano
//{
//    interface IModel
//    {
//        void Attach(IEventObserver observerView);
//    }

//    public class BeepNonStatic
//    {
//        public event EventHandler changed;

//        //private SoundPlayer SP = new SoundPlayer();
//        private SoundPlayer SP;
//        private BinaryWriter BW;
//        private MemoryStream memoryStream;
//        private List<byte> byteArray = new List<byte>();

//        private double deltaFT;

//        public BeepNonStatic()
//        {
//              CreateSoundPlayer();
//             var optimalLoopDuration = 300;
//             Samples = GetSamples(optimalLoopDuration);
                
//        }

//        private void CreateSoundPlayer()
//        {
//            SP = new SoundPlayer();
//        }
//        // > 100 throws exception
//        private short VOLUME_AMOUNT = 100;
//        private double workingAmplitude;
//        private int Samples;
//        private double tempStrength;

//        public void Play(double frequency, double duration)
//        {
//            //var t = new Thread(() => BeepBeep(frequency));
//            //t.Start();
//            BeepBeep(frequency);
//        }

//        public void Stop()
//        {
//            //SP.Stop();
//            //var currentPlayed = SP.Stream.Position;
//            //if (currentPlayed != SP.Stream.Length)
//            //{
//            //  SP.Stream.Position = SP.Stream.Length-5;
//            //SP.Stream.Seek(SP.Stream.Length - 3, SeekOrigin.Begin);
//            //SP.Load();

//            //}

//            //memoryStream.Position = SP.Stream.Length - 5;
//            //SP.Stream = memoryStream;
//            //SP.Play();


//            //SP.Stream.Seek(12, SeekOrigin.);
//            //if(SP.Stream.Position == SP.Stream.Length - 1)  
//            //
//        }

//        private void StartMonitoringChanges()
//        {
//            //monitor playing of stream in player class
//            bool isRunning = true;

//            Thread t = new Thread(delegate()
//            {
//                Thread.Sleep(100);

                
//                Thread.Sleep(200);

//                if (changed != null)
//                {
//                    changed(this, EventArgs.Empty);
//                }
                
                
//                //while (isRunning)
//                //{
//                //    //if (this.SP.Stream.Position > 300)
//                //    //    Debug.WriteLine("hghgh");

//                //    Debug.WriteLine("Position.... " + this.SP.Stream.Position);

//                //    //if (this.SP.Stream.Position > SP.Stream.Length - 3)
//                //    //{
//                //    //    if (changed != null)
//                //    //    {
//                //    //        changed(this, EventArgs.Empty);
//                //    //    }

//                //    //    isRunning = false;
//                //    //}

//                //    /// System.Windows.Forms.MessageBox.Show("hgh");


//                //    //FirePropertyChange();
//                //    //SetEmployeeTo
//                //    //(
//                //    //    firstNames[rand.Next(firstNames.Length)],
//                //    //    lastNames[rand.Next(lastNames.Length)]
//                //    //);
//                //    //Thread.Sleep(DELAY);
//                //}
//            });


//            //Thread t = new Thread(() =>
//            //    Console.Write("sdf")
//            //    );
//            //t.IsBackground = false;
//            t.Start();
//        }

//        public void Attach(IEventObserver observerView)
//        {
//            changed += observerView.HandleEvent;
//        }


//        private void BeepBeep(double Frequency)
//        {
//            WriteToneToStream(Frequency, false);
//            SetStreamToTheBegining();

//            //System.IO.File.WriteAllBytes("tone.wav", memoryStream.ToArray());

//            SetSoundPlayerStream();
            
//            //SP.SoundLocation = "tone.wav";

//            //SP.Stream.Seek(12, SeekOrigin.Current);

//            //var thread = new Thread(StartMonitoringChanges);
//            //thread.IsBackground = false;
//            //thread.Priority = ThreadPriority.Normal;
//            //thread.Start();

           

//            PlaySoundPlayer();




//            StartMonitoringChanges();
            


//            //changed.Invoke(this, EventArgs.Empty);

          
                
//            //SP.Stream.

//            //Dispose();
//        }

//        private void SetSoundPlayerStream()
//        {
//            SP.Stream = memoryStream;
//        }

//        private void SetStreamToTheBegining()
//        {
//            memoryStream.Seek(0, SeekOrigin.Begin);
//        }

//        private void PlaySoundPlayer()
//        {
//            SP.Play();
//        }

//        private double Amplitude
//        {
//            get
//            {
//                return ((VOLUME_AMOUNT * (System.Math.Pow(2, 15))) / 100) - 1;
//            }
//        }

//        private double GetDeltaFT(double frequency)
//        {
//            var samplesPerSecond = 44100.0;
//            var lenghtOfCurve = 2 * Math.PI;

//            var deltaFT = lenghtOfCurve * frequency / samplesPerSecond;

//            return deltaFT;
//        }

//        #region Scheme
//        //values var is equivalent to Samples
//        //end of every phase is defined by CurrentItem in iterator
//        //
//        // attack   decay sustain  release
//        //---------   ----  ----- -----> t
//        //           -
//        //         -   -
//        //       -      -
//        //     -          ------ 
//        //   -                   -
//        // -                       - 

//        //... so with iterator pattern i dont need phase vars 
//        #endregion

//        private void WriteActualToneToWriter(short Sample)
//        {
//            BW.Write(Sample);
//            BW.Write(Sample);
//        }

//        private void WriteToneToStream(double frequency, bool EndPart)
//        {
//            workingAmplitude = Amplitude;

//            this.deltaFT = GetDeltaFT(frequency);

//            int Bytes = PrepareStreamWithSamples(Samples);
//            WriteWavHeaderToStream(Bytes);

//            double minSound = 0.0;

//            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!
//            var realAttackDuration = PhaseDuration(AttackPhase.duration);
//            var realDecayDuration = realAttackDuration + PhaseDuration(DecayPhase.duration);
//            var realSustainDuration = realDecayDuration + PhaseDuration(SustainPhase.duration);
//            var realReleaseDuration = realSustainDuration + PhaseDuration(SustainPhase.duration);

//            for (int T = 0; T < Samples; T++)
//            {
//                if (T < realAttackDuration)
//                {
//                    minSound += AttackPhase.strength / PhaseDuration(AttackPhase.duration);
//                }

//                else if (T > realAttackDuration && T < realDecayDuration)
//                {
//                    minSound += -(AttackPhase.strength - DecayPhase.strength) / PhaseDuration((double)DecayPhase.duration);
//                }

//                else if (T > realDecayDuration && T < realSustainDuration)
//                {
//                    minSound += -(DecayPhase.strength - SustainPhase.strength) / PhaseDuration((double)SustainPhase.duration);
//                }

//                else if (T > realSustainDuration && T < realReleaseDuration && minSound > 0)
//                {
//                    minSound += -(SustainPhase.strength - ReleasePhase.strength) / PhaseDuration((double)ReleasePhase.duration);
//                }
//                var Sample = GetFinalSamples(minSound, this.deltaFT, T);

//                BW.Write(Sample);
//                BW.Write(Sample);
//            }

//            BW.Flush();
//        }

//        private double PhaseDuration(double duration)
//        {
//            return Samples * duration;
//        }

//        public static short GetFinalSamples(double amplitude, double deltaFT, double T)
//        {
//            return System.Convert.ToInt16(amplitude * Math.Sin(deltaFT * T));
//        }

//        private static int GetSamples(double Duration)
//        {
//            return (int)(441.0 * Duration / 10.0);
//        }

//        private int PrepareStreamWithSamples(int Samples)
//        {
//            int Bytes = Samples * sizeof(int);
//            memoryStream = new MemoryStream(44 + Bytes);
//            BW = new BinaryWriter(memoryStream);
//            return Bytes;
//        }

//        private void WriteWavHeaderToStream(int Bytes)
//        {
//            int[] wavHeader = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
//            for (int I = 0; I < wavHeader.Length; I++)
//            {
//                BW.Write(wavHeader[I]);
//            }
//        }

//        public void Dispose()
//        {
//            SP.Dispose();
//            //BW.Dispose();
//        }
//    }
//}
