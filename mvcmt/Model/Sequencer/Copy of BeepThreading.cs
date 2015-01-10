using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;
using System.Threading;

namespace Bit8Piano
{
    //better way to play sound than Console.Beep, Which is not working for 64bit windows
    public class BeepThreadingOriginal
    {
        private static SoundPlayer SP;
        private static BinaryWriter BW;
        private static MemoryStream MS;

        public BeepThreadingOriginal()
        {
            SP = new SoundPlayer();
            SP.Play();
        }

        // > 1000 throws an exception - custom type
        private const short OPTIMAL_AMPLITUDE = 1000;
        private List<byte> byteArray = new List<byte>();

        public void Play(double frequency, double duration)
        {
            BeepBeep(frequency, 1);
        }

        public void Stop()
        {
            SP.Stop();
        }

        private void BeepBeep(double Frequency, double Duration)
        {
            WriteToneToStream(Frequency, Duration);

            MS.Seek(0, SeekOrigin.Begin);

            System.IO.File.WriteAllBytes("tone.wav", MS.ToArray());

            SP.Stream = MS;
            SP.Play();
        }

        private void WriteToneToStream(double Frequency, double Duration)
        {
            double Amp = ((OPTIMAL_AMPLITUDE * (System.Math.Pow(2, 15))) / 1000) - 1;
            double DeltaFT = 2 * Math.PI * Frequency / 44100.0;

            int Samples = (int)(441.0 * Duration / 10.0);
            int Bytes = Samples * sizeof(int);
            int[] Hdr = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };

            MS = new MemoryStream(44 + Bytes);

            BW = new BinaryWriter(MS);

            for (int I = 0; I < Hdr.Length; I++)
            {
                BW.Write(Hdr[I]);
            }

            ///!!!! ANY CHANGE TO THE FOR LOOP CHANGES AMPLITUDE TOO !!!
            for (int T = 0; T < Samples; T++)
            {
                short Sample = System.Convert.ToInt16(Amp * Math.Sin(DeltaFT * T));
                byte[] tempByteArray = BitConverter.GetBytes(Sample);

                byteArray.AddRange(tempByteArray);
                byteArray.AddRange(tempByteArray);
            }
            
            BW.Write(byteArray.ToArray());
            BW.Flush();
        }

        public void Dispose()
        {
            SP.Dispose();
            BW.Dispose();
        }
    }
}
