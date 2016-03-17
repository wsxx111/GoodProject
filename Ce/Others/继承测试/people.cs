using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    class people
    {
       public virtual void alive()
       {
           Console.WriteLine("父类还活着!");
       }
       public  void speak()
       {
           Console.WriteLine("父类说话了！");
       }
       public void death()
       {
           Console.WriteLine("父类肯定有一天会死掉的！");
       }
    }
}
