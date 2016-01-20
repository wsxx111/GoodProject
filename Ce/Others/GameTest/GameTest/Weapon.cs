using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    public class Weapon:IWeapon
    {
        private int _cuthp;
        private string _name;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="weaponname"></param>
        /// <param name="cuthp"></param>
        public Weapon(string weaponname,int cuthp)
        {
            this._cuthp = cuthp;
            this._name = weaponname;
        }

        /// <summary>
        /// 武器损血量
        /// </summary>
        public int CutHp
        {
            get
            {
                return _cuthp;
            }
            set
            {
                _cuthp=value;
            }
        }

        /// <summary>
        /// 武器名称
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
