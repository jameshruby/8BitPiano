using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleApplication1
{
    public class IteratorSample : IEnumerable
    {
        public object[] values;
        public int startingPoint;

        public IteratorSample(object[] values, int startingPoint)
        {
            this.values = values;
            this.startingPoint = startingPoint;
        }

        public IEnumerator GetEnumerator()
        {
            return new IteratorSampleIterator(this);
        }
    
    
    
    
    }
}
