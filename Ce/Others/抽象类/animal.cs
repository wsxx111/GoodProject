using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 抽象与继承 
{
   public class animal :shengming
    {
       public animal()
       {
           Console.WriteLine("animal被构造");
       }

       //子类必须实现父类中的所有的抽象方法，即abstract的方法
         public string chi;
          public override void live()
        {
           Console.WriteLine( "animal类必须实现live的live()抽象方法");
        }
          public override void death()
          {
              Console.WriteLine("animal类必须实现live的death()抽象方法");
          }

          public override  void birth()  //重写父类shengming中的虚方法用override  //重写后将以实方法看待
          {
              Console.WriteLine("animal完成对shengming类中的birth()虚方法的重写");
          }

          public new void huozhe()   //覆盖父类shenming中的huozhe()的方法 实例化时，用该方法
          {
              Console.WriteLine("animal 活着");
          }          
    }
}
