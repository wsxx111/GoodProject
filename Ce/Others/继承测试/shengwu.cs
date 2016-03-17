using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    abstract  class shengwu
    {
        public abstract void shuijiao();
        public void fasheng()
        {
            Console.WriteLine("生物会发声！");
        }
        public void buru()
        {
            Console.WriteLine("生物会养育小崽！");
        }
    }
}
