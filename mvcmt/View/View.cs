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

    class View : Form, IEventObserver
    {
        #region Private Fields

        private string[] KeysControlingPiano = new string[8] { "a", "s", "d", "f", "g", "h", "j", "k" };

        private string[] ButtonKeysText = new string[] { "C", "D", "E", "F", "G", "A", "H", "C" };

        private int i = 0;

        private IBeatModel beatModel;
        private IBeatController beatController;

        private KeyboardButton button1;
        private KeyboardButton button2;
        private KeyboardButton button3;
        private KeyboardButton button4;
        private KeyboardButton button5;
        private KeyboardButton button6;
        private KeyboardButton button7;
        private KeyboardButton button8;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;

        //private TimerkeyDownTimer ;
        //private Control[] pianoKeysButtons = new Control[]{}; 
        private List<Control> pianoKeysButtons = new List<Control>();
        private bool keyDownOnce = true;
        private Button exitButton;
        private Panel results;

        #endregion

        #region Private methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();

            //foreach (var buttonKeyText in ButtonKeysText)
            //{

            //}
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumOrchid;
            this.ClientSize = new System.Drawing.Size(588, 328);



            this.exitButton = new Button();
            this.exitButton.BackColor = System.Drawing.Color.DarkOrchid;
            this.exitButton.BackgroundImageLayout = ImageLayout.Center;

            //this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Plum;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.exitButton.Width = 52;
            this.exitButton.Height = 22;
            this.exitButton.Image = System.Drawing.Bitmap.FromFile(@"C:\Github\Bit8Piano\mvcmt\Resources\icon2.gif");

            this.exitButton.Location = new System.Drawing.Point(this.Width - (this.exitButton.Width + 8), 0);
            this.exitButton.Click += new EventHandler(button0_Click);


            //this.settingsButton = new Button();
            //this.exitButton.Width = 41;
            //this.exitButton.Width = 41;
            //this.exitButton.Location = new System.Drawing.Point(this.Width - this.exitButton.Width, 0);
            //this.exitButton.Click += new EventHandler(button0_Click);


            this.button1 = new Bit8Piano.PianoKeyButton("C");
            this.button2 = new Bit8Piano.PianoKeyButton("D");
            this.button3 = new Bit8Piano.PianoKeyButton("E");
            this.button4 = new Bit8Piano.PianoKeyButton("F");
            this.button5 = new Bit8Piano.PianoKeyButton("G");
            this.button6 = new Bit8Piano.PianoKeyButton("A");
            this.button7 = new Bit8Piano.PianoKeyButton("H");
            this.button8 = new Bit8Piano.PianoKeyButton("C");

            this.button1.MouseDown += button_Click;
            this.button1.MouseUp += button_Up;

            this.button2.MouseDown += button_Click;
            this.button2.MouseUp += button_Up;

            this.button3.MouseDown += button_Click;
            this.button3.MouseUp += button_Up;

            this.button4.MouseDown += button_Click;
            this.button4.MouseUp += button_Up;

            this.button5.MouseDown += button_Click;
            this.button5.MouseUp += button_Up;

            this.button6.MouseDown += button_Click;
            this.button6.MouseUp += button_Up;

            this.button7.MouseDown += button_Click;
            this.button7.MouseUp += button_Up;

            this.button8.MouseDown += button_Click;
            this.button8.MouseUp += button_Up;


            this.pianoKeysButtons.Add(this.button1);
            this.pianoKeysButtons.Add(this.button2);
            this.pianoKeysButtons.Add(this.button3);
            this.pianoKeysButtons.Add(this.button4);
            this.pianoKeysButtons.Add(this.button5);
            this.pianoKeysButtons.Add(this.button6);
            this.pianoKeysButtons.Add(this.button7);
            this.pianoKeysButtons.Add(this.button8);

            // results = new Panel();
            //results.Width = 40;
            //results.Height = 50;
            //results.Visible = true;

            this.Controls.Add(this.exitButton);
            //this.Controls.Add(this.results);
            this.Controls.AddRange(pianoKeysButtons.ToArray<Control>());
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "8s8b8k";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.KeyDown += View_KeyDown;
            this.KeyUp += View_KeyUp;

        }

        void button0_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Up(object sender, MouseEventArgs e)
        {
            beatController.Stop();
        }

        void button_Click(object sender, EventArgs e)
        {
            //block keys wuthout blocking whole UI thread
            //count time and unblock it again

            DisablePianoKeys();

            foreach (var pianoKeyButton in this.pianoKeysButtons)
            {
                if (Object.ReferenceEquals(sender, pianoKeyButton))
                {
                    Button f = (Button)pianoKeyButton;
                    f.BackColor = System.Drawing.Color.GreenYellow;
                    beatController.PerformActionWithStrategy(f.TabIndex);
                }
            }
           
        }

        private delegate void EnablePianoKeysDelegate();
        private void EnablePianoKeys()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EnablePianoKeysDelegate(EnablePianoKeys),
                                          new object[] { });
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
                default:
                    throw new NotSupportedException("Theres no button assigned for this key");
            }
            return buttonActivatedByKey;
        }

        void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.keyDownOnce == true)
            {

                //should call c++ function for getting physical keyboard layout, in order to keep it working at any possible keyboard
                if (KeysControlingPiano.Contains(e.KeyCode.ToString().ToLower()))
                {
                    var customIndex = Array.IndexOf(KeysControlingPiano, e.KeyCode.ToString().ToLower());

                    var actualButtonForKey = UsedKeysActivationStrategy(customIndex);
                    actualButtonForKey.PerformKeyDown();
                }
                ///  this.KeyDown -= View_KeyDown;
                this.keyDownOnce = false;
            }
        }

        void View_KeyUp(object sender, KeyEventArgs e)
        {
            this.keyDownOnce = true;

            if (KeysControlingPiano.Contains(e.KeyCode.ToString().ToLower()))
            {
                var customIndex = Array.IndexOf(KeysControlingPiano, e.KeyCode.ToString().ToLower());

                var actualButtonForKey = UsedKeysActivationStrategy(customIndex);
                actualButtonForKey.PerformKeyUp();
            }
        }

        #endregion

        public string TopEmployeeName
        {
            get; // { }return label1.Text; 
            set; // { }label1.Text = value; 
        }

        public View(IBeatController beatController, IBeatModel beatModel)
        {
            this.beatController = beatController;
            this.beatModel = beatModel;

            beatModel.OnPropertyChange += new Action(UpdateView);

            InitializeComponent();
        }

        public void UpdateView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateView));
            }
            else
            {
                TopEmployeeName = beatModel.FullName;
            }
        }


        public void HandleEvent(object sender, EventArgs e)
        {
            EnablePianoKeys();
        }
    }
}
