using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Bit8Piano
{
    interface IIterator
    {
        int FirstItem { get; }
        int NextItem { get; }
        int CurrentItem { get; }
        int CurrentIndex { get; }
        bool IsDone { get; }
    }
}
