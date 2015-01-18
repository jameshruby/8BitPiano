using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano
{
    class ConcreteIterator : IIterator
    {
        private ICollection collection;
        private int currentIndex;
        private int iteratorStep;

        public int IteratorStep { get { return iteratorStep; } set { iteratorStep = value; } }

        public ConcreteIterator(ICollection aggregate)
        {
            this.collection = aggregate;
        }

        public int FirstItem
        {
            get
            {
                currentIndex = 0;
                return Int32.Parse(collection[currentIndex]);
            }
        }

        public int NextItem
        {
            get
            {
                currentIndex += iteratorStep;

                if (IsDone == false)
                {
                    return Int32.Parse(collection[currentIndex]);
                }
                else
                {
                    return 0;
                }
            }
        }

        public int CurrentItem
        {
            get
            {
                return Int32.Parse(collection[currentIndex]);
            }
        }

        public int CurrentIndex { get { return currentIndex; } }

        public bool IsDone
        {
            get
            {
                if (currentIndex < collection.Count || currentIndex >= 0)
                    return false;
                return true;
            }
        }
    }
}
