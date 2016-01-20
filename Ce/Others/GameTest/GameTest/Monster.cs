using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    /// <summary>
    /// 怪兽
    /// </summary>
    public class Monster : IMonster
    {

        private int _havehp;
        private string _name;
        private bool _state;

        /// <summary>
        /// 是否爆袭
        /// </summary>
        /// <returns></returns>
        public bool IsBoom()
        {
            Random random = new Random();    
            int ss=random.Next(4);
            return ss>=2?true:false;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name"></param>
        /// <param name="havehp"></param>
        public Monster(string name, int havehp)
        {
            if (havehp > 0)
            {
                this._name = name;
                this._havehp = havehp;
                this._state = true;
                Console.WriteLine("怪兽{0}诞生,PH:{1}", _name,_havehp);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 怪兽的HP值
        /// </summary>
        int IMonster.HaveHp
        {
            get
            {
                return _havehp;
            }
            set
            {
                _havehp = value;
            }
        }

        /// <summary>
        /// 怪兽的名字
        /// </summary>
        string IObject.Name
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

        /// <summary>
        /// 怪兽的状态
        /// </summary>
        public bool State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        /// <summary>
        /// 怪兽遭到袭击
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public string GetAttack(Role role)
        {
            if (this._state)
            {
                if (IsBoom())
                {
                    this._havehp -= role.HaveWeapon.CutHp * 2;
                    if (this._havehp > 0)
                    {
                        return String.Format("怪兽{0}被{1}用{2}爆袭了一次，还有PH：{3}", _name, role.Name, role.HaveWeapon.Name, _havehp);
                    }
                    else
                    {
                        this._state = false;
                        return String.Format("怪兽{0}已死", _name);
                    }
                }
                else
                {
                    this._havehp -= role.HaveWeapon.CutHp;
                    if (this._havehp > 0)
                    {
                        return String.Format("怪兽{0}被{1}用{2}普袭了一次，还有PH：{3}", _name, role.Name, role.HaveWeapon.Name, _havehp);
                    }
                    else
                    {
                        this._state = false;
                        return String.Format("怪兽{0}已死", _name);
                    }
                }
            }
            else
            {
                return string.Empty;
            }
        }

    }
}
