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

            RunConsole(piano);
        }

        private static void RunConsole(Piano piano)
        {
            while (true)
            {
                Console.Write("PianoConsole>: ");
                userInput = Console.ReadLine();

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
