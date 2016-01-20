using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    public class Role:IRole
    {
        private string _name;
        private Weapon _haveWeapon;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="haveweapon"></param>
        public Role(string name,Weapon haveweapon)
        {
            this._name = name;
            this._haveWeapon = haveweapon;
        }

        /// <summary>
        /// 拿的武器
        /// </summary>
        public Weapon HaveWeapon
        {
            get
            {
                return _haveWeapon;
            }
            set
            {
                _haveWeapon = value;
                Console.WriteLine("{0}成功换取了{1}武器",_name,_haveWeapon.Name);
            }
        }

        /// <summary>
        /// 人物的名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
    }
}
