using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bit8Piano
{
    class PianoKeyButton : KeyboardButton
    {
        private static int UniqueTabIndex = 1;

        private static int positionX = 28;
        private static int positionY = 28;

        private const int margin = 6;

        private const int width = 61;
        private const int height = 279;

        private const int position = width + margin;


        private static System.Drawing.Point UniqueLocation = new System.Drawing.Point(8, 8);

        //public PianoKeyButton()
        //{
           
        //}

        public PianoKeyButton(string text)
        {
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GreenYellow;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.Location = new System.Drawing.Point(positionX, positionY);
            this.Size = new System.Drawing.Size(width, height);
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Text = text;
            this.Font = new System.Drawing.Font("Palatino Linotype", 100F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;

            this.UseVisualStyleBackColor = false;
            //this.MouseDown += button_Click;
            //this.MouseUp += button_Up;

            this.TabIndex = UniqueTabIndex;

            UniqueTabIndex++;
            positionX += position;
        }
    }
}
