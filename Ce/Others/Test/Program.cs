using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public delegate void MyDelegate(object sender, MyEventArgs e);

    public class MyEventArgs : EventArgs
    {
        //公有 只读
        public readonly decimal oldprice;
        public readonly decimal newprice;
        public MyEventArgs(decimal Oldprice, decimal Newprice)
        {
            this.oldprice = Oldprice;
            this.newprice = Newprice;
        }
    }

    public class Iphone6
    {
        public event MyDelegate myevent;

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnChange(new MyEventArgs(oldPrice, price));
            }
        }

        protected void OnChange(MyEventArgs ev)
        {
            if (myevent != null)
            {
                myevent(this, ev);
            }
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Iphone6 iph = new Iphone6() { Price = 5332m };
            iph.myevent += DisPrice;
            iph.Price = 3445m;
        }

        public static  void DisPrice(object sender, MyEventArgs e)
        {
            Console.WriteLine("原来价格"+e.oldprice+",新价格是"+e.newprice);
        }
    }
}
