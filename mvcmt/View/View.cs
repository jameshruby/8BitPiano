using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bit8Piano
{
    class View : Form
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
           
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            
            this.pianoKeysButtons.Add(this.button1);
            this.pianoKeysButtons.Add(this.button2);
            this.pianoKeysButtons.Add(this.button3);
            this.pianoKeysButtons.Add(this.button4);
            this.pianoKeysButtons.Add(this.button5);
            this.pianoKeysButtons.Add(this.button6);
            this.pianoKeysButtons.Add(this.button7);
            this.pianoKeysButtons.Add(this.button8);


            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumOrchid;
            this.ClientSize = new System.Drawing.Size(548, 294);
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

        private void button_Up(object sender, MouseEventArgs e)
        {
            beatController.Stop();
        }

        void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.button1.Text = i++.ToString();
        }

        void button_Click(object sender, EventArgs e)
        {
            //beatController.GetTopEmloee();

            foreach (var pianoKeyButton in this.pianoKeysButtons)
            {
                if (Object.ReferenceEquals(sender, pianoKeyButton))
                {
                   Button f = (Button)pianoKeyButton;
                   beatController.PerformActionWithStrategy(f.TabIndex);
                }
            }

            //var minDurationOfKeyPress = 100;
            //Thread.Sleep(1000);
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
            //should call c++ function for getting physical keyboard layout, in order to keep it working at any possible keyboard
            if (KeysControlingPiano.Contains(e.KeyCode.ToString().ToLower()))
            {
                var customIndex = Array.IndexOf(KeysControlingPiano, e.KeyCode.ToString().ToLower());

                var actualButtonForKey = UsedKeysActivationStrategy(customIndex);
                actualButtonForKey.PerformKeyDown();
            }
        }

        void View_KeyUp(object sender, KeyEventArgs e)
        {
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
    }
}
