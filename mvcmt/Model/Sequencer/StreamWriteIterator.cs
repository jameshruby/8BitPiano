using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Bit8Piano
{
    class StreamWriteIterator : IEnumerator
    {
        //parent
        private IteratorSample iteratorSample;
        private double position;

        public StreamWriteIterator(IteratorSample iteratorSample)
        {
            this.iteratorSample = iteratorSample;
        }
        public object Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            position += iteratorSample.iterationSteps;

            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
