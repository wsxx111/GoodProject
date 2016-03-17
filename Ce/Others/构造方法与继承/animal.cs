using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 构造方法与继承
{
    class animal
    {
        public animal()
        {
            Console.WriteLine("animal类被构造");
        }
        public animal(int ss)
        {
            Console.WriteLine("animal类 带一个参数 的被构造");
        }
    }
}
