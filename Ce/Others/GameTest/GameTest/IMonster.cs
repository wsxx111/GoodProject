using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest
{
    /// <summary>
    /// 怪兽接口
    /// </summary>
    interface IMonster:IObject
    {     
        /// <summary>
        /// 怪兽的HP值
        /// </summary>
        int HaveHp { get; set;}

        /// <summary>
        /// 怪兽遭到袭击方法
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        string GetAttack(Role role);
    }
    
}
