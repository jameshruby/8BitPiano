using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bit8Piano
{
    public interface IEventObserver
    {
        void HandleEvent(object sender, EventArgs e);
    }

    partial class View : Form
    {
        private string[] KeysControlingPiano = new string[] { "a", "s", "d", "f", "g", "h", "j", "k", "w", "e", "r", "y", "u" };

        private IBeatModel beatModel;
        private IBeatController beatController;

        void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void button0_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Up(object sender, MouseEventArgs e)
        {
            beatController.Stop();
        }


        private void button_Click(object sender, EventArgs e)
        {
            //DisablePianoKeys();
            foreach (var pianoKeyButton in this.pianoKeysButtons)
            {
                if (Object.ReferenceEquals(sender, pianoKeyButton))
                {
                    Button f = (Button)pianoKeyButton;
                    beatController.PerformActionWithStrategy(f.TabIndex);
                }
            }
        }

        private delegate void EnablePianoKeysDelegate();
        private void EnablePianoKeys()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EnablePianoKeysDelegate(EnablePianoKeys), new object[] { });
                return;
            }
            pianoKeysButtons.ForEach(key => { key.Enabled = true; key.BackColor = System.Drawing.Color.White; });
        }

        public void DisablePianoKeys()
        {
            pianoKeysButtons.ForEach(key => key.Enabled = false);
        }

        private KeyboardButton UsedKeysActivationStrategy(int customIndex)
        {
            KeyboardButton buttonActivatedByKey;

            switch (customIndex)
            {
                case 0:
                    buttonActivatedByKey = this.button1;
                    break;
                case 1:
                    buttonActivatedByKey = this.button2;
                    break;
                case 2:
                    buttonActivatedByKey = this.button3;
                    break;
                case 3:
                    buttonActivatedByKey = this.button4;
                    break;
                case 4:
                    buttonActivatedByKey = this.button5;
                    break;
                case 5:
                    buttonActivatedByKey = this.button6;
                    break;
                case 6:
                    buttonActivatedByKey = this.button7;
                    break;
                case 7:
                    buttonActivatedByKey = this.button8;
                    break;
                case 8:
                    buttonActivatedByKey = this.button9;
                    break;
                case 9:
                    buttonActivatedByKey = this.button10;
                    break;
                case 10:
                    buttonActivatedByKey = this.button11;
                    break;
                case 11:
                    buttonActivatedByKey = this.button12;
                    break;
                case 12:
                    buttonActivatedByKey = this.button13;
                    break;
                default:
                    throw new NotSupportedException("Theres no button assigned for this key");
            }
            return buttonActivatedByKey;
        }

        void View_KeyDown(object sender, KeyEventArgs e)
        {
            //if (this.keyDownOnce == true)
            //{

            //should call c++ function for getting physical keyboard layout, in order to keep it working at any possible keyboard
            if (KeysControlingPiano.Contains(e.KeyCode.ToString().ToLower()))
            {
                var customIndex = Array.IndexOf(KeysControlingPiano, e.KeyCode.ToString().ToLower());

                var actualButtonForKey = UsedKeysActivationStrategy(customIndex);
                actualButtonForKey.PerformKeyDown();
            }

            //this.keyDownOnce = false;
            //}
        }

        void View_KeyUp(object sender, KeyEventArgs e)
        {
            //this.keyDownOnce = true;

            if (KeysControlingPiano.Contains(e.KeyCode.ToString().ToLower()))
            {
                var customIndex = Array.IndexOf(KeysControlingPiano, e.KeyCode.ToString().ToLower());

                var actualButtonForKey = UsedKeysActivationStrategy(customIndex);
                actualButtonForKey.PerformKeyUp();
            }
        }

        public View(IBeatController beatController, IBeatModel beatModel)
        {
            this.beatController = beatController;
            this.beatModel = beatModel;

            InitializeComponent();
        }

    }
}
