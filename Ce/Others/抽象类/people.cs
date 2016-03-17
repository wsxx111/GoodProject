using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 抽象与继承
{   //覆盖与重写的不同 在形式上
    public sealed class people :animal
    {
        public void shuo()
        {
            Console.WriteLine("people中的shuo()方法");
        }

        public people()
        {
            Console.WriteLine("people被构造");
        }
        public new void birth()  //对animal完成shengming中的birth()虚方法进行重写后的birth()(已变为实方法）进行覆盖
        {
            Console.WriteLine("people中覆盖animal重写后的birth()虚方法");
        }
        public override void death() //people同样可以重写animal中的death的shengming的抽出方法的重新实现
        {
            Console.WriteLine("people对animal中实现shengming的death()抽象方法重新实现");
        }
        public new void huozhe()   //覆盖父类animal中的huozhe()的方法 实例化时，用该方法
        {
            Console.WriteLine("people 活着");
        }
    }
}
