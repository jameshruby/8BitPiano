using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsolePiano
{
    class Piano
    {
        List<IPianoKey> keyboard = new List<IPianoKey>();

        public List<IPianoKey> GetKeys { get { return keyboard; } }

        public void AddKey(IPianoKey pianoKey)
        {
            var keyExists = keyboard.Any(key => key.Tone == pianoKey.Tone);

            if (keyExists)
                throw new Exception("A key with same tone already exists");
            else
                keyboard.Add(pianoKey);
        }

        public void PlayKey(MusicalTone tone)
        {
            IPianoKey specifiedKey = keyboard.SingleOrDefault(key => key.Tone == tone);
            
            if (specifiedKey == null)
                throw new Exception("This key is not at the keyboard!");
            else
                specifiedKey.Play();
        }
    }
}
