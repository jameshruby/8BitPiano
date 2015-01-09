using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Media;

namespace Bit8Piano
{
    //better way to play sound than Console.Beep, Which is not working for 64bit windows
    public static class Beep
    {
        // I added this function because amplitude should just always be 1000
        // < 1000 sounds muffled and > 1000 throws an exception
        public static void Play(double frequency, double duration)
        {
            BeepBeep(1000, frequency, duration);
        }



        private static void BeepBeep(double Amplitude, double Frequency, double Duration)
        {
            double Amp = ((Amplitude * (System.Math.Pow(2, 15))) / 1000) - 1;
            double DeltaFT = 2 * Math.PI * Frequency / 44100.0;

            int Samples = (int)(441.0 * Duration / 10.0);
            int Bytes = Samples * sizeof(int);
            int[] Hdr = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };

            using (MemoryStream MS = new MemoryStream(44 + Bytes))
            {
                using (BinaryWriter BW = new BinaryWriter(MS))
                {
                    for (int I = 0; I < Hdr.Length; I++)
                    {
                        BW.Write(Hdr[I]);
                    }
                    for (int T = 0; T < Samples; T++)
                    {
                        short Sample = System.Convert.ToInt16(Amp * Math.Sin(DeltaFT * T));
                        BW.Write(Sample);
                        BW.Write(Sample);
                    }

                    BW.Flush();
                    MS.Seek(0, SeekOrigin.Begin);
                    using (SoundPlayer SP = new SoundPlayer(MS))
                    {
                        SP.Play();
                    }
                }
            }
        }
    }
}
