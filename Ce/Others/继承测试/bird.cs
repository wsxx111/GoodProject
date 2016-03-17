using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    class bird :shengwu
    {
        public override void shuijiao()  //实现父类抽象方法
        { 
        Console.WriteLine("鸟也需要睡觉");
        }
        public void fasheng()  //覆盖
        {
            Console.WriteLine("鸟的发声！");
        }
        public void fly()
        {
            Console.WriteLine("鸟会飞！");
        }

    }
}
