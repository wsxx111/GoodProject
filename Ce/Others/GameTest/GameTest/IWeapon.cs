using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    /// <summary>
    /// 武器接口
    /// </summary>
    interface IWeapon:IObject
    {
        /// <summary>
        /// 武器损伤HP的值
        /// </summary>
        int CutHp{get;set;}
    }
}
