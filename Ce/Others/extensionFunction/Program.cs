using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using extensionFunction;

namespace FunctionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string age = "48";
            int getage = age.StringToInt();
            Console.WriteLine(getage);

            int height = 34;
            int newheight = height.IntAddNum(45);
            Console.WriteLine(newheight);

            Console.ReadKey();
        }
    }
}

//  1.创建扩展方法
/*
一个静态类中，建一个静态方法，静态类其实没用的，静态方法中第一个参数是以this修饰的，其类型就是调用其的对象的类型，其他有参数的话，就是调用时的要传的参数，
 *第一个参数就是说明调用者本身的,,扩展方法的第一个参数是要扩展的类型，放在this关键字的后面，告诉编译期这个方法是Money类型的一部分.
*/

//  2.调用扩展方法
/*
 * 引用该扩展方法所在的命名空间，用该扩展方法中指定的类型对象去调用这个方法，不需要管该扩展方法属于哪个类。这与类无关。
 * */


/*   “扩展方法使您能够向现有类型“添加”方法，而无需创建新的派生类型、重新编译或以其他方式修改原始类型。”

 这是msdn上说的，也就是你可以对String,Int,DataRow,DataTable等这些类型的基础上增加一个或多个方法，也可以是自定义的数据类型。使用时不需要去修改或编译类型本身的代码。

 扩展方法规定类必须是一个静态类,里面包含的所有方法都必须是静态方法.

  扩展方法被定义为静态方法，但它们是通过实例方法语法进行调用的。 它们的第一个参数指定该方法作用于哪个类型，并且该参数以 this 修饰符为前缀. this是(如string)实例化后的对象,
 * 扩展方法的第一个参数是要扩展的类型，放在this关键字的后面，告诉编译期这个方法是Money类型的一部分*/


