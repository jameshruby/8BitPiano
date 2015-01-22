using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio;
using System.IO;

namespace FireAndForgetAudioSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream1 = new MemoryStream(File.ReadAllBytes("boom.wav"));
            stream1.Position = 0;
            var waveStream1 = new WaveFileReader(stream1);
            waveStream1.Position = 0;
        
            var stream2 = new MemoryStream(File.ReadAllBytes("zap.wav"));
            stream2.Position = 0;
            var waveStream2 = new WaveFileReader(stream2);
            waveStream2.Position = 0;

            var stream3 = new MemoryStream(File.ReadAllBytes("crash.wav"));
            stream3.Position = 0;
            var waveStream3 = new WaveFileReader(stream3);
            waveStream3.Position = 0;

            List<IWaveProvider> sounds = new List<IWaveProvider>();
           
            
            sounds.Add(waveStream1);
            //sounds.Add(waveStream2);

            var waveProvider = new MultiplexingWaveProvider(sounds, 2);
            waveProvider.ConnectInputToOutput(1, 1);
            //waveProvider.ConnectInputToOutput(2, 1);
      
            var waveOut1 = new WaveOut();
            waveOut1.Init(waveProvider);
            waveOut1.Play();
            System.Threading.Thread.Sleep(200);

            sounds.Add(waveStream3);
            waveProvider = new MultiplexingWaveProvider(sounds, 2);
            waveProvider.ConnectInputToOutput(1, 1);
            waveProvider.ConnectInputToOutput(2, 1);

            //waveOut1 = new WaveOut();
            waveOut1.Init(waveProvider);
           
          
            //waveProvider.ConnectInputToOutput(2, 1);
            
            //var waveOut2 = new WaveOut();
            //waveOut2.Init(waveStream2);

         
            //waveOut2.Volume = 0.33f;


            waveOut1.Play();
            //waveOut2.Play();
          

            //System.Threading.Thread.Sleep(3000);

            //waveOut2.Stop();
            //waveOut3.Stop();
            //waveOut3.Dispose();
            //waveOut2.Dispose();
            
            
            //waveOut2.Play();

            //var _mixer = new WaveMixerStream32 { AutoStop = false, };
            //var _waveOutDevice = new WaveOut(WaveCallbackInfo.NewWindow())
            //{
            //    DeviceNumber = -1,
            //    DesiredLatency = 300,
            //    NumberOfBuffers = 3,
            //};
            //_waveOutDevice.Init(_mixer);
            //_waveOutDevice.Play();

            //var sample = new AudioSample();
            //sample.Position = sample.Length; // To prevent the sample from playing right away

            //// on startup:
            //var zap = new CachedSound("zap.wav");
            //var boom = new CachedSound("boom.wav");

            ////// later in the app...
            //AudioPlaybackEngine.Instance.PlaySound(zap);
            //AudioPlaybackEngine.Instance.PlaySound(boom);
            //////AudioPlaybackEngine.Instance.PlaySound("crash.wav");

            Console.ReadLine();

            // on shutdown
            //AudioPlaybackEngine.Instance.Dispose();

        }
    }
}
