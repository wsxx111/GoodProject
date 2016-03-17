using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 继承测试
{
    class Program
    {
       
        static void Main(string[] args)
        {
            people p1 = new people();
            p1.alive();
            p1.speak();
            
            Console.WriteLine("..................");
            man m1 = new man();
            m1.alive();
            m1.speak();
            m1.eat();
            Console.WriteLine("..................");

            //对于父类对象引用子类实例，创建的对象不能调用子类中特有的方法。
            //可见people p2 = new man()不再是man类型了
            //但是p2可以调用父类的实方法和子类中实现的（override）方法
            //不管子类中是否覆盖了父类的方法，p2仍然调用父类的方法
            people p2 = new man();
            p2.alive();   //子类重写父类的虚函数  //父类方法已被改变，等同于调用子类的方法
            p2.death();  //调用父类中特有的方法  //可以调用
            p2.speak();  //子类覆盖父类的实函数 //仍然调用父类的是函数
          //  p2.eat();  不能调用子类中特有的方法，可见已经不属于子类了


            //相似的，对于父类是抽象类的研究
            //同上一个相同
            //对于抽象的类中如果含有实方法，又含有抽象方法，除了继承的子类实例
            //可以调用外，父类对象引用子类实例也可以调用被
            //子类实现化后抽象的方法和父类的实方法，但不能调用子类特有的方法。
            Console.WriteLine("........另一个........");
            bird b1 = new bird();
            b1.shuijiao();
            b1.shuijiao();
            b1.fly();
            b1.fasheng();
            Console.WriteLine("..................");
            shengwu b2 = new bird(); 
            b2.shuijiao();
            b2.fasheng();
            b2.buru();
          //b2.fly();

            //关于多层继承中含有多层抽象类的研究
            //对于抽象类的多层继承而言，只要是一个抽象的类就可以不实现
            //父类及以上类的抽象方法，只有不是抽象类，这样的话，必须实现
            //所有的在继承中还没有过被实现的抽象方法。
            Console.WriteLine("........第三个例子........");
            Console.WriteLine("...轿车...");
            jiaoche j1 = new jiaoche();
            j1.chelun();
            j1.jiage();
            j1.pao();
            j1.shengchandi();
            j1.shouming();
            j1.yanse();
            j1.zhuren();
            Console.WriteLine("...四个轮的车去引用...");
            silunci s1 = new jiaoche();
            s1.chelun();
            s1.jiage();
            s1.pao();
            s1.shengchandi();
            s1.shouming();
            s1.yanse();
         // s1.zhuren();
            Console.WriteLine("...车去引用...");
            car c1 = new jiaoche();
            c1.jiage();
            c1.shengchandi();
            c1.shouming();
            //不能调用下几代中本类没有的方法
            Console.ReadKey();

        }
    }
}
