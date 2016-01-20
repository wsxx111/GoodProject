using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 标准委托事件
{
    public class PriceChangeEventArgs : EventArgs
    {
        public readonly decimal oldprice;
        public readonly decimal newprice;
        public PriceChangeEventArgs(decimal oldPrice, decimal newPrice)
        {
            oldprice = oldPrice;
            newprice = newPrice;
        }
    }

    /// <summary>
    /// 为事件定义委托，必须满足以下条件：
    /// 必须是 void 返回类型
    /// 必须有两个参数，且第一个是object类型，第二个是EventArgs类型（的子类）
    /// 它的名称必须以EventHandler结尾。
    /// </summary>

    public delegate void MyPriceChangeEventHandler(object sender, PriceChangeEventArgs e);
    //或者用泛类型
    public delegate void MyOtherEventHandler<PriceChangeEventArgs>(object source, PriceChangeEventArgs e);

    public class Iphone6
    {
        public event MyPriceChangeEventHandler pricechangevent;
        //或者
        public event MyOtherEventHandler<PriceChangeEventArgs> pricechangevent2;

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                decimal oldprice = price;
                price = value;
                if (pricechangevent != null)
                {
                    //执行事件
                    pricechangevent(this, new PriceChangeEventArgs(oldprice, price));
                }
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Iphone6 iph = new Iphone6() { Price = 5445m };
            //事件订阅   对象也是（object sender,PriceChangeArgs e）
            iph.pricechangevent += iphone_DisCount;
            //开始监听
            //事件触发 的制造
            iph.Price = 3334m;
        }

        static void iphone_DisCount(object sender, PriceChangeEventArgs e)
        {
            Console.WriteLine("大出血。Iphone只卖" + e.newprice + ",原价：" + e.oldprice + "元");
        }
    }
}
