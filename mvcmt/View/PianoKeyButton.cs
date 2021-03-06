﻿using System;
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

        private static int positionX = 6;
        private static int positionY = 40;

        private const int margin = 6;

        private const int width = 61;
        private const int height = 279;

        private const int position = width + margin;

        public virtual int PositionX { get { return positionX; } }
        public  virtual int WidthC{ get { return width;  } }
        public virtual int HeightC { get { return height; } }
        
        public PianoKeyButton(string text)
        {
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FlatAppearance.CheckedBackColor = System.Drawing.Color.WhiteSmoke;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.GreenYellow;
            this.FlatAppearance.MouseOverBackColor =   this.BackColor;
            this.Location = new System.Drawing.Point(PositionX, positionY);
            this.Size = new System.Drawing.Size(WidthC, HeightC);
            this.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Text = text;
            this.Font = new System.Drawing.Font("Palatino Linotype", 25);
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
