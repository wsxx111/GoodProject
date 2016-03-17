using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    class man :people
    {
        public override void alive()
        {
            base.alive();
            Console.WriteLine("子类也活着!");
        }
        public void speak()
        {
            Console.WriteLine("子类说话了！");
        }
        public void eat()
        {
            Console.WriteLine("子类还会吃！");
        }

    }
}
