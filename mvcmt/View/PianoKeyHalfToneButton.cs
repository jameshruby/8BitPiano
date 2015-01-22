using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Bit8Piano
{
    class PianoKeyHalfToneButton : PianoKeyButton
    {
        private Color backColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
        private static int positionXd = 43;
        private const int width = 50;
        private const int height = 180;
        public override Color BackColor { get { return backColor; } }

        private int marginr = 18;

        //public override int PositionX
        //{
        //    get
        //    {
        //        positionX += position;
        //        return positionX;
        //    }
        //}

        public override int WidthC
        {
            get
            {
                return width;
            }
        }

        public override int HeightC
        {
            get
            {
                return height;
            }
        }

        public PianoKeyHalfToneButton(string text)
            : base(text)
        {

            if (this.Text.Contains("F#"))
                marginr += 3 * marginr + marginr / 2;
            this.Location = new System.Drawing.Point(positionXd, 40);
            this.Font = new System.Drawing.Font("Palatino Linotype", 10);
            this.TextAlign = ContentAlignment.TopCenter;
            positionXd += width + marginr;

            this.ForeColor = System.Drawing.Color.WhiteSmoke;
        }
    }
}
