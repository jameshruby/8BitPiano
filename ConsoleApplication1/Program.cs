using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IteratorPatternGof
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteCollection collection = new ConcreteCollection();
   
            collection[0] = "1";
            collection[1] = "2";
            collection[2] = "3";
            collection[3] = "4";
            collection[4] = "5";
            collection[5] = "6";

            IIterator iterator = collection.GetEnumerator();

            for (string text = iterator.FirstItem; iterator.IsDone == false; text = iterator.NextItem)
            {
                Console.WriteLine(text);
            }
        }
    }
}
