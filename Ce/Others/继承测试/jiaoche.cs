using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    class jiaoche :silunci 
    {
        public override void jiage()    //实现爷爷的抽象方法
        {
            Console.WriteLine("轿车的价格。 实现爷爷的抽象方法");
        }
        public override void chelun()  //子类实现父亲的抽象方法
        {
         Console.WriteLine("轿车有四个轮子 实现父亲的抽象方法"); 
        }
        public void pao()   //子类覆盖父亲的方法
        {
            Console.WriteLine("轿车的四个轮能滚动  子类覆盖父亲的实方法");
        }
        public void zhuren()   //子类特有的实方法
        {
            Console.WriteLine("轿车的主人是谁 子类特有的实方法");
        }


    }
}
