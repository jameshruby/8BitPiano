using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bit8Piano
{
    class SamplesCollection : ICollection
    {
        private int[] values;

        public SamplesCollection(int size)
        {
            values = new int[size];
        }

        public IIterator GetEnumerator()
        {
            return new ConcreteIterator(this);
        }

       
        public int Count
        {
            get
            {
                return values.Length;
            }
        }


        public string this[int itemIndex]
        {
            get
            {
                if (itemIndex < values.Length)
                {
                    return values[itemIndex].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                values[itemIndex] = Int32.Parse(value);
            }
        }
    }
}
