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
        public ConcreteIterator(ICollection aggregate)
        {
            this.collection = aggregate;
        }
        
        public int FirstItem
        {
            get {
                currentIndex = 0;
                return Int32.Parse(collection[currentIndex]);
            }
        }

        public int NextItem
        {
            get {
                currentIndex += 1;

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

        public int CurrentIndex { get{ return currentIndex; }}

        public bool IsDone
        {
            get
            {
                if (currentIndex < collection.Count)
                    return false;
                return true;
            }
        }
    }
}
