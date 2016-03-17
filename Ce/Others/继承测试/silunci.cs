using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    abstract class silunci :car
    {
        public abstract void chelun();  //父亲特有的抽象方法
        public void yanse()  //父亲特有的方法
        {
            Console.WriteLine("四轮车都有颜色  四轮车特有的实方法");
        }
        public void pao()   //父亲准备的被子类覆盖的方法
        {
            Console.WriteLine("四轮车能滚动");
        }
        public void shouming()   //父亲覆盖他爸爸的实方法
        {
            Console.WriteLine("车轮成也是有寿命的  四轮车覆盖车的实方法 ");
        }
    }
}
