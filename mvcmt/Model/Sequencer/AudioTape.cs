using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit8Piano
{
    // Encapsulate musical data so Im exposing only things that i have to
    public class AudioTape
    {
        public List<KeyValuePair<Tone, Duration>> Notes = new List<KeyValuePair<Tone, Duration>>();
        private DanceTempo tempo = DanceTempo.FinnishWaltz;
      
        public AudioTape(DanceTempo danceTempo)
        {
            this.tempo = danceTempo;
        }

        public DanceTempo Tempo { get { return tempo; } set { tempo = value; } }

        public void AddNote(Tone tones, Duration duration)
        {
            Notes.Add(new KeyValuePair<Tone, Duration>(tones, duration));
        }
        
        //I thing its funnier to use dance tempo as tempo of audiotape than regular music tempo
        public enum DanceTempo
        {
            Merengue = 248,
            Quickstep = 240,
            Polka = 240,
            Mambo = 232,
            Salsa = 210,
            Dixieland = 200,
            Samba = 200,
            FrenchWaltz = 192,
            Schottische = 180,
            VienneseWaltz = 180,
            SeamansWaltz = 180,
            Tijuana = 172,
            Reggae = 172,
            Two_StepFoxtrot = 170,
            Twist = 170,
            Baion = 168,
            Rumba = 162,
            Mazurka = 162,
            FinnishWaltz = 150,
            GermanTango = 132,
            Conga = 128,
            Cha_Cha = 120,
            FinnishTango = 120,
            March = 120,
            ArgentinianTango = 108,
            Beguine = 108,
            Calypso = 104,
            SlowWaltz = 90,
            Foxtrot = 80,
            Bolero = 80,
        }
     }

    // Outside Frequency Range: C#0/Db0, D#0/Eb0, F#0/Gb0, G#0/Ab0, A#0/Bb0, C#1/Db1
    public enum Tone
    {
              SILENCE = 0,
        Dsharp1, Eb1 = 39,      
                  E1 = 41,
                  F1 = 44,
        Fsharp1, Gb1 = 46,
                  G1 = 49,
        Gsharp1, Ab1 = 52,      
                  A1 = 55,
        Asharp1, Bb1 = 58,    
                  B1 = 62,

                  C2 = 65,
        Csharp2, Db2 = 69,      
                  D2 = 73,
        Dsharp2, Eb2 = 78,      
                  E2 = 82,
                  F2 = 87,
        Fsharp2, Gb2 = 92,      
                  G2 = 98,
        Gsharp2, Ab24 = 104,    
                   A2 = 110,
        Asharp2, Bb2 = 116,     
                  B2 = 123,

        C3 = 131,
        Csharp3 = 139,     //Db3
        D3 = 147,
        Dsharp3 = 155,     //Eb3
        E3 = 165,
        F3 = 175,
        Fsharp3 = 185,     //Gb3
        G3 = 196,    //GbelowC 
        Gsharp3 = 208,     //Ab3
        A3 = 220,
        Asharp3 = 233,     //Bb3
        B3 = 247,      //H3

        C = 262,     // Middle C <≡══───-─ -
        Csharp, Db = 277,
        D = 294,
        Dsharp, Eb, Cm = 311,
        E = 330,
        F = 349,
        Fsharp, Gb = 370,
        G = 392,
        Gsharp, Ab = 415,
        A = 440,
        Asharp, Bb, Gm = 466,
        B = 494,

        // '5' is probably mol(minor part)
                  C5 = 523,
        Csharp5, Db5 = 554,  
                  D5 = 587,
        Dsharp5, Eb5 = 622, 
                  E5 = 659,
              F5, Fm = 698,
        Fsharp5, Gb5 = 740,  
                  G5 = 784,
        Gsharp5, Ab5 = 831, 
                  A5 = 880,
        Asharp5, Bb5 = 932, 
                  B5 = 988,

             C6 = 1046,
        Csharp6 = 1109,    ///Db6
             D6 = 1175,
        Dsharp6 = 1245,    //Eb6
             E6 = 1319,
             F6 = 1397,
        Fsharp6 = 1480,    //Gb6
             G6 = 1568,
        Gsharp6 = 1661,    //Ab6
             A6 = 1760,
        Asharp6 = 1865,    //Bb6
             B6 = 1976,

             C7 = 2093,
        Csharp7 = 2217,    //Db7
             D7 = 2349,
        Dsharp7 = 2489,    //Eb7
             E7 = 2637,
             F7 = 2794,
        Fsharp7 = 2960,    //Gb7
             G7 = 3136,
        Gsharp7 = 3322,    ///Ab7
             A7 = 3520,
        Asharp7 = 3729,    //Bb7
             B7 = 3951,
             C8 = 4186,
        Csharp8 = 4435,    //Db8
             D8 = 4699,
        Dsharp8 = 4978,    //Eb8
    }
    // Define duration for an octave
    public enum Duration
    {
        WHOLE = 60000 * 4,
        HALF = WHOLE / 2,
        QUARTER = HALF / 2,
        EIGHTH = QUARTER / 2,
        SIXTEENTH = EIGHTH / 2,

        HALFDOT = HALF + QUARTER,
    }
}
