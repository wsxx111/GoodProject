using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    /// <summary>
    /// 人物接口
    /// </summary>
    interface IRole:IObject
    {
        /// <summary>
        /// 拿的是声明武器
        /// </summary>
        Weapon HaveWeapon{get;set;}      
    }
}
