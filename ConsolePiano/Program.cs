using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsolePiano
{
    class Program
    {
        private static string userInput;
        static void Main(string[] args)
        {
            var piano = new Piano();

            var d = new Dictionary<string, MusicalTone>();

            //every key need to have unique alias AND music tone
           
            piano.AddKey(new PianoKey(MusicalTone.C, PianoKey.Type.White, "C"));
            piano.AddKey(new PianoKey(MusicalTone.D, PianoKey.Type.White, "D"));
            piano.AddKey(new PianoKey(MusicalTone.E, PianoKey.Type.White, "E"));
            piano.AddKey(new PianoKey(MusicalTone.F, PianoKey.Type.White, "F"));
            piano.AddKey(new PianoKey(MusicalTone.G, PianoKey.Type.White, "G"));
            piano.AddKey(new PianoKey(MusicalTone.A, PianoKey.Type.White, "A"));
            piano.AddKey(new PianoKey(MusicalTone.B, PianoKey.Type.White, "H"));
            piano.AddKey(new PianoKey(MusicalTone.C5, PianoKey.Type.White, "C5"));

            piano.AddKey(new PianoKey(MusicalTone.Csharp, PianoKey.Type.Black, "C#"));

            piano.GetDefaultKeyboardShorcuts();

            piano.PlayKey("C");

            //Testing code
            //string path1 = "lastTone.wav";
            //string path2 = "lastTone - Copy.wav";
            //string path3 = "lastTone_releasePhase5096.wav";
            //string path4 = "tone.wav";
            //if (!System.IO.File.ReadAllBytes(path1).SequenceEqual(System.IO.File.ReadAllBytes(path2)) && 
            //    !System.IO.File.ReadAllBytes(path1).SequenceEqual(System.IO.File.ReadAllBytes(path3)) &&
            //    !System.IO.File.ReadAllBytes(path1).SequenceEqual(System.IO.File.ReadAllBytes(path4))
            //   )
            //{
            //    throw new Exception("Files no match");
            //}
           

            //RunConsole(piano);
        }

        private static void RunConsole(Piano piano)
        {
            while (true)
            {
                Console.Write("PianoConsole>: ");
                userInput = "/play -test";// Console.ReadLine();
                //TODO overriding user input
                if (userInput == "/play -alias")
                {
                    while (true)
                    {
                        Console.WriteLine("PianoConsole>Write a note you want to play");

                        Console.Write("PianoConsole>: ");

                        userInput = Console.ReadLine();

                        if (userInput == "/back")
                            break;

                        try
                        {
                            piano.PlayKey(userInput);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                if (userInput == "/play -test")
                {
                    Console.Write("Play-test:");
                    while (true)
                    {
                        try
                        {
                            Console.Write("Play-test:");

                            /*
                                
                                so, now, the      duration
                                piano.PlayKey("C", 10)
                                this method wraps pplay and then call stop
                                //sleep 10
                               
                                piano.HoldKey("C");
                                
                                piano.ReleaseKey("C");
                            */

                            piano.PlayKey("C");
                            //piano.PlayKey("D");
                            //var delayWhileIstrumentPlayds = 500;
                            //System.Threading.Thread.Sleep(delayWhileIstrumentPlayds);
                            //piano.PlayKey("C");
                          
                            //var delayWhileIstrumentPlays = 1000;
                            //System.Threading.Thread.Sleep(delayWhileIstrumentPlays);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    //piano.PlayKey("D");   
                    //piano.PlayKey("C");
                    //piano.PlayKey("D");

                }

                if (userInput == "/play -keyboard")
                {
                    while (true)
                    {
                        Console.Write("PianoConsole>: ");

                        var inputKey = Console.ReadKey();
                        Keys activeKey = (Keys)inputKey.Key;

                        try
                        {
                            piano.PlayKey(activeKey);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }


                if (userInput == "/set")
                {
                    while (true)
                    {
                        Console.WriteLine("PianoConsole>: Set tone for hotkey");
                        Console.Write("PianoConsole>: ");

                        var keyForHotkey = Console.ReadKey();

                        Console.WriteLine("PianoConsole>: Set key for tone {0}", keyForHotkey);
                        Console.Write("PianoConsole>: ");

                        var inputHotkey = Console.ReadKey();
                        Keys desiredHotkey = (Keys)inputHotkey.Key;

                        piano.SetHotkey(keyForHotkey.KeyChar.ToString(), desiredHotkey);
                    }
                } 
            }
        }
    }
}
