﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Bit8Piano
{
    interface IIterator
    {
        string FirstItem { get; }
        string NextItem { get; }
        string CurrentItem { get; }
        bool IsDone { get; }
    }
}
