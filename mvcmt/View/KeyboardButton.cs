using System;
using System.Windows.Forms;

namespace Bit8Piano
{
    class KeyboardButton : Button
    {
        public void PerformKeyDown()
        {
            this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 0, 1, 1, 0));
        }

        public void PerformKeyUp()
        {
            this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 0, 1, 1, 0));
        }
    }
}
