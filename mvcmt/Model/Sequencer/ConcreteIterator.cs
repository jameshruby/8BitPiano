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
        
        public string FirstItem
        {
            get {
                currentIndex = 0;
                return collection[currentIndex];
            }
        }

        public string NextItem
        {
            get {
                currentIndex += 1;

                if (IsDone == false)
                {
                    return collection[currentIndex];
                }
                else
                {
                    return string.Empty; 
                }
            }
        }

        public string CurrentItem
        {
            get
            {
                return collection[currentIndex];
            }
        }

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
