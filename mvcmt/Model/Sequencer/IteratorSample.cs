using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Bit8Piano
{
    class IteratorSample : IEnumerable
    {
        private int[] collection;
        private int startingPoint;
        public double iterationSteps;
        public IteratorSample(int[] collection, int startingPoint)
        {
            this.collection = collection;
            this.startingPoint = startingPoint;
        }
        
        public IEnumerator GetEnumerator()
        {
            return new StreamWriteIterator(this);
        }
    }
}
