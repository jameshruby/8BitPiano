using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IteratorPatternGof
{
    class ConcreteCollection : ICollection
    {
        private List<string> values;

        public ConcreteCollection()
        {
            values = new List<string>();
        }

        public IIterator GetEnumerator()
        {
            return new ConcreteIterator(this);
        }

        public string this[int itemIndex]
        {
            get
            {
                if (itemIndex < values.Count)
                {
                    return values[itemIndex];
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                values.Add(value);
            }
        }

        public int Count
        {
            get
            {
                return values.Count;
            }
        }
    }
}
