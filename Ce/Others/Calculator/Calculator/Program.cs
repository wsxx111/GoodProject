using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("请输入数字A：");
                string strNumberA = Console.ReadLine();
                Console.Write("请选择运算符号(+、-、*、/)：");
                string strOperate = Console.ReadLine();
                Console.Write("请输入数字B：");
                string strNumberB = Console.ReadLine();
                string strResult = "";
                strResult = Convert.ToString(
                OldOperation.GetResult(Convert.ToDouble(strNumberA), Convert.ToDouble(strNumberB), strOperate));
                Console.WriteLine("结果是：" + strResult);
                Console.ReadLine();  
              


                //利用工厂模式

                Operation oper = OperationFactory.createOperate("+");
                oper.NumberA = 1;
                oper.NumberB = 2;
                double result = oper.GetResult();
                Console.WriteLine("结果是：" + result);
                Console.ReadLine(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("您的输入有错：" + ex.Message);
            }
        }


        public class OldOperation
        {
            public static double GetResult(double numberA, double numberB, string operate)
            {
                double result = 0d;
                switch (operate)
                {
                    case "+":
                        result = numberA + numberB;
                        break;
                    case "-":
                        result = numberA - numberB;
                        break;
                    case "*":
                        result = numberA * numberB;
                        break;
                    case "/":
                        result = numberA / numberB;
                        break;
                }
                return result;
            }
        }

    }
}
