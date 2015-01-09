using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1;

namespace IteratorPatternGof
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] values = {"a" , "b", "c" , "d", "e"};

            IteratorSample collection = new IteratorSample(values, 3);

            foreach (object text in collection)
            {
                  Console.WriteLine(text);
            }
            Console.Read();
        }
    }
}
