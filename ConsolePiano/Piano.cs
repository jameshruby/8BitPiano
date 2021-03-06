﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsolePiano.InstrumentalNote;
using System.Windows.Forms;

namespace ConsolePiano
{
    public class Piano
    {
        private List<IPianoKey> keyboard = new List<IPianoKey>();
        private Dictionary<string, Keys> keyboardHotkeys = new Dictionary<string, Keys>();

        private DefaultInstrumentNote instrumentNote;


        public List<IPianoKey> GetKeys { get { return keyboard; } }

        public Piano()
        {
            //Piano creates one audio stream with ADSR phases which is modulated by 
            //speciic frequency of given note of played key

            //TODO I should be able to compute duration from the actual ADSR phases
            instrumentNote = new DefaultInstrumentNote(200);
        }

        public void GetDefaultKeyboardShorcuts()
        {
            var startingKeyPoint = Keys.A;

            for (int i = 0; i < keyboard.Count; i++)
            {
                keyboardHotkeys.Add(keyboard[i].Alias, startingKeyPoint + i);
            }
        }

        public void SetHotkey(string keyAlias, Keys hotkey)
        {
            if (!keyboardHotkeys.ContainsKey(keyAlias))
                throw new Exception("A key with specified alias doesnt exists");

            keyboardHotkeys[keyAlias] = hotkey;
        }

        public void AddKey(IPianoKey pianoKey)
        {
            var keyExists = keyboard.Any(key => key.Tone == pianoKey.Tone);

            if (keyExists)
                throw new Exception("A key with same tone already exists");
            else
                keyboard.Add(pianoKey);
        }

        public void PlayKey(Keys hotkey)
        {
            string alias = keyboardHotkeys.SingleOrDefault(hkey => (int)hkey.Value == (int)hotkey).Key;
            PlayKey(alias);
        }

        public void PlayKey(string alias)
        {
            IPianoKey specifiedKey = keyboard.SingleOrDefault(key => key.Alias == alias);
            if (specifiedKey == null)
                throw new Exception("This key is not at the keyboard!");
            else
                specifiedKey.Play(instrumentNote);
        }
    }
}
