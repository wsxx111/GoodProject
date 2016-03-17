using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    abstract  class car
    {
        public abstract void jiage();  //爷爷的抽象方法

        public void shouming()   //爷爷准备的被下一代覆盖方法
        {
            Console.WriteLine("车是有寿命的");
        }
        public void shengchandi() //爷爷特有的方法
        {
            Console.WriteLine("车的生产地  爷爷特有的方法 ");
        }
        
    }
}
