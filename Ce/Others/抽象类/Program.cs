using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 抽象与继承
{  //覆盖不会改变父类的方法   重写会改变父类的方法  当以子类创建父类的时候会体现出来
    class Program
    {
        static void Main(string[] args)
        {
            // people p1 =new people();   shengming被构造  animal被构造   people被构造
          //  shengming s1 = new shengming();   作为abstract的抽象类不能被声明
            animal liv1 = new animal();     //shengming被构造  animal被构造
            animal p2 = new people();    //子类创建父类对象
            liv1.death();  //输出animal 中的
            liv1.huozhe();  //输出animal 中的
            liv1.live();   //输出animal 中的
            liv1.birth();  //输出animal 中的
            Console.WriteLine("---------------------");
            people p1 = new people();
            p1.death();   //输出people 中的
            p1.huozhe();  //输出people 中的
            p1.live();    //输出继承animal 中的  //由于没有重写
            p1.birth();    //输出people 中的
            Console.WriteLine("---------------------");
            liv1.death();   //输出animal 中的
            liv1.huozhe();  //输出animal 中的
            liv1.live();   //输出animal 中的
            liv1.birth();  //输出animal 中的           
            Console.WriteLine("---------------------");
            p2.death();//输出people 中的
            p2.huozhe();//输出animal 中的
            p2.live();//输出animal 中的
            p2.birth();//输出animal中的
            Console.WriteLine("-----------------------");
            Console.WriteLine("---------转换----------");

         
           //  people p3 = new animal();  //出错子类引用不能直接引用父类对象，  除非将父类对象的数据类型强制转换成子类
           // people p4 = (people)liv1; // 如果你创建实例的时候没有将父类引用到子类对象,是无法转换的
            //  p2.shuo();          //错误，引用不了, 父类引用自雷对象时无法子类中的新方法
            people p5 = (people)p2;   //这样转后，能调用 子类中的新方法，说明父类引用时没用丢到子类中的新方法，只是不能调用
            p5.shuo();
            Console.ReadKey();
        }
    }
}
