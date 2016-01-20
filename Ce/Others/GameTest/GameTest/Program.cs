using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameTest
{
    class Program
    {
        static void Main()
        {
            var timer = Stopwatch.StartNew();           
            Monster monster = new Monster("大鬼王", 150);
            Role role1 = new Role("小绅士", new Weapon("木剑", 5));
            Role role2 = new Role("小哈哈", new Weapon("钢剑", 10));
            Console.WriteLine(monster.GetAttack(role1));
            Console.WriteLine(monster.GetAttack(role2));
            Console.WriteLine(monster.GetAttack(role2));         
            role1.HaveWeapon = new Weapon("神盾", 12);
            Console.WriteLine(monster.GetAttack(role1));
            Console.WriteLine(monster.GetAttack(role2));
            Console.WriteLine(monster.GetAttack(role1));
            Console.WriteLine(monster.GetAttack(role2));
            Console.WriteLine(monster.GetAttack(role1));
            timer.Stop();
            Console.WriteLine(timer.ElapsedTicks);
            Console.ReadKey();
        }
    }
}
