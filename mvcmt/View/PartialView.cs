using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bit8Piano
{
    partial class View : Form
    {
        private KeyboardButton button1;
        private KeyboardButton button2;
        private KeyboardButton button3;
        private KeyboardButton button4;
        private KeyboardButton button5;
        private KeyboardButton button6;
        private KeyboardButton button7;
        private KeyboardButton button8;
        private KeyboardButton button9;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;

        //private TimerkeyDownTimer ;
        //private Control[] pianoKeysButtons = new Control[]{}; 
        private List<Control> pianoKeysButtons = new List<Control>();

        private Button exitButton;
        private Panel results;
        private Button settingsButton;
        private Button minimizeButton;
        private KeyboardButton button10;
        private KeyboardButton button11;
        private KeyboardButton button12;
        private KeyboardButton button13;
        private KeyboardButton button14;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();

            //
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumOrchid;
            this.ClientSize = new System.Drawing.Size(542, 326);

            this.exitButton = new Button();
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.BackColor = System.Drawing.Color.MediumOrchid;
            this.exitButton.BackgroundImageLayout = ImageLayout.Center;
            this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrchid;
            this.exitButton.FlatAppearance.BorderSize = 1;
            this.exitButton.Width = 52;
            this.exitButton.Height = 22;
            this.exitButton.Image = System.Drawing.Bitmap.FromFile(@"C:\Github\Bit8Piano\mvcmt\Resources\iconExit2.gif");

            this.exitButton.Location = new System.Drawing.Point(this.Width - (this.exitButton.Width + 8), 0);
            this.exitButton.Click += new EventHandler(button0_Click);

            this.minimizeButton = new Button();
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.BackColor = System.Drawing.Color.MediumOrchid;
            this.minimizeButton.BackgroundImageLayout = ImageLayout.Center;
            this.minimizeButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrchid;
            this.minimizeButton.FlatAppearance.BorderSize = 1;
            this.minimizeButton.Width = 52;
            this.minimizeButton.Height = 22;
            this.minimizeButton.Image = System.Drawing.Bitmap.FromFile(@"C:\Github\Bit8Piano\mvcmt\Resources\iconMinimize.gif");

            this.minimizeButton.Location = new System.Drawing.Point(this.Width - (this.exitButton.Width + 2 + 52), 0);
            this.minimizeButton.Click += new EventHandler(minimizeButton_Click);


            this.settingsButton = new Button();
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.BackColor = System.Drawing.Color.MediumOrchid;
            this.settingsButton.BackgroundImageLayout = ImageLayout.Center;
            this.settingsButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrchid;
            this.settingsButton.FlatAppearance.BorderSize = 1;
            this.settingsButton.Width = 52;
            this.settingsButton.Height = 22;
            this.settingsButton.Image = System.Drawing.Bitmap.FromFile(@"C:\Github\Bit8Piano\mvcmt\Resources\iconSettings.gif");
            this.settingsButton.Location = new System.Drawing.Point(1, 0);
            this.settingsButton.Click += new EventHandler(button0_Click);


            this.button1 = new Bit8Piano.PianoKeyButton("C");
            this.button2 = new Bit8Piano.PianoKeyButton("D");
            this.button3 = new Bit8Piano.PianoKeyButton("E");
            this.button4 = new Bit8Piano.PianoKeyButton("F");
            this.button5 = new Bit8Piano.PianoKeyButton("G");
            this.button6 = new Bit8Piano.PianoKeyButton("A");
            this.button7 = new Bit8Piano.PianoKeyButton("H");
            this.button8 = new Bit8Piano.PianoKeyButton("C");

            this.button9 = new Bit8Piano.PianoKeyHalfToneButton("C#");
            this.button10 = new Bit8Piano.PianoKeyHalfToneButton("D#");
            this.button11 = new Bit8Piano.PianoKeyHalfToneButton("F#");
            this.button12 = new Bit8Piano.PianoKeyHalfToneButton("G#");
            this.button13 = new Bit8Piano.PianoKeyHalfToneButton("A#");



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

            this.button9.MouseDown += button_Click;
            this.button9.MouseUp += button_Up;

            this.button10.MouseDown += button_Click;
            this.button10.MouseUp += button_Up;

            this.button11.MouseDown += button_Click;
            this.button11.MouseUp += button_Up;

            this.button12.MouseDown += button_Click;
            this.button12.MouseUp += button_Up;

            this.button13.MouseDown += button_Click;
            this.button13.MouseUp += button_Up;


            this.pianoKeysButtons.Add(this.button1);
            this.pianoKeysButtons.Add(this.button2);
            this.pianoKeysButtons.Add(this.button3);
            this.pianoKeysButtons.Add(this.button4);
            this.pianoKeysButtons.Add(this.button5);
            this.pianoKeysButtons.Add(this.button6);
            this.pianoKeysButtons.Add(this.button7);
            this.pianoKeysButtons.Add(this.button8);

            this.pianoKeysButtons.Add(this.button9);
            this.pianoKeysButtons.Add(this.button10);
            this.pianoKeysButtons.Add(this.button11);
            this.pianoKeysButtons.Add(this.button12);
            this.pianoKeysButtons.Add(this.button13);

            // results = new Panel();
            //results.Width = 40;
            //results.Height = 50;
            //results.Visible = true;

            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.AddRange(pianoKeysButtons.ToArray<Control>());
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Paint += View_Paint;

            this.Text = "8s8b8k";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.KeyDown += View_KeyDown;
            this.KeyUp += View_KeyUp;
            this.Controls.SetChildIndex(button9, 2);
            this.Controls.SetChildIndex(button10, 2);
            this.Controls.SetChildIndex(button11, 2);
            this.Controls.SetChildIndex(button12, 2);
            this.Controls.SetChildIndex(button13, 2);
        }

        void View_Paint(object sender, EventArgs e)
        {
            //    var myObject = sender as PianoKeyButton;
            //    if (myObject != null)
            //    {
            //        //# successfully cast
            //        //Button b = (PianoKeyButton2)sender;
            //        myObject.BringToFront();

            //    }
            //    else
            //    {
            //        //#cast failed
            //    }

            //this.button8 = new Bit8Piano.PianoKeyButton("C");
            //if (sender is KeyboardButton)
            //{
            //    MessageBox.Show("blahb vblhja");

            ////    //Button f = (Button)pianoKeyButton;
            ////    //beatController.PerformActionWithStrategy(f.TabIndex);
            //}



            //if (sender.Text == "C#")
            //    MessageBox.Show("blahb vblhja");
            //beatController.PerformActionWithStrategy(f.TabIndex);
        }

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

    }

}
