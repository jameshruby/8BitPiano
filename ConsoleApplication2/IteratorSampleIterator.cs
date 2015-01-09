using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleApplication1
{
    class IteratorSampleIterator : IEnumerator
    {
         IteratorSample parent;
         int position;

        //usage of nested classes
        internal IteratorSampleIterator(IteratorSample parent)
        {
            this.parent = parent;
        }

        public object Current
        {
            get 
            {
                if (position == -1 ||
                position == parent.values.Length)
                {
                    throw new InvalidOperationException();
                }
                int index = position + parent.startingPoint;
                index = index % parent.values.Length;

                return parent.values[index];
            }
        }

        public bool MoveNext()
        {
            if (position != parent.values.Length)
            {
                position++;
            }
            return position < parent.values.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
