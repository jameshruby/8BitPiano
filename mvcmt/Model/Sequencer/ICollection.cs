using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IteratorPatternGof
{
    interface ICollection
    {
        IIterator GetEnumerator();
        string this[int itemIndex] { get; set; }
        int Count { get; }
    }
}
