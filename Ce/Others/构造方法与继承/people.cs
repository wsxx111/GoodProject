using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 构造方法与继承
{
      /*四个构造方法同时写时，没有语法错误，但在实例化对象时，会报错，实例化时，只看本类的构造方法
    而如果本类的构造方法中有两个相同的（带参或不带参），虽然它们继承父类的不同构造，但对实例化而言
    只找有参的和无参的，选择相应的构造函数。*/

    class people :animal
    {  
        //此时，被继承的animal的构造函数必须声明为public
       //public  people() :base(6)
       // {
       //     Console.WriteLine("people类 无参 animal类 带参 被构造");
       // }
       public people(string aa): base(6)
       {
           Console.WriteLine("people类 带参 animal类 带参 被构造");
       }
       public people()
       {
           Console.WriteLine("people类 无参 animal 无参 被构造");
       }
       //public people(string aa)
       //{
       //    Console.WriteLine("people类 带参 animal无参 被构造");
       //}
    }
}
