using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 构造方法与继承
{
    class Program
    {
        static void Main(string[] args)
        {
            people p1 = new people();
            people p2 = new people("yell");

            Console.ReadKey();
        }
    }
}
