using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 抽象与继承
{
    public abstract class shengming   //抽象类
    {  //抽象类中不是所有的方法都是抽象方法，对于单纯抽象类而言，是限制类的实例化，可以没有抽象方法。
        //而抽象方法的目的是为了让派生类实现其方法，抽象的方法不实现。
        //包含抽象方法的类一定是抽象类。
        //可以包含一些非抽象方法和字段，以及属性
        public string name;

        public abstract void live(); //抽象方法  没有方法体 没有大括号 故下面的实现不能有
        //{   
        //}
        public abstract void death();

        //对于虚方法，可以存在于抽象类中，也可以存在于非抽象类中
        public virtual void birth()
        {  //首先虚方法 是一种方法，不是抽象的，它有大括号，是实现的，子类可以重写，可以覆盖
          //虚方法是父类中不一定继承到子类中的，具体由子类决定 
            //它与抽象方法不同，虚方法与一般的方法相比，可实现由子类选择是否继承
            Console.WriteLine("shengming birth()虚方法");
        }

        public void huozhe()
        {
            Console.WriteLine("shengming 活着");
        }
        public shengming()
        {
            Console.WriteLine("shengming 被构造");
        }
    }
}
