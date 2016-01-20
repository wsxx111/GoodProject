using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial interface IUserNOSQLStrategy
    {
        /// <summary>
        /// 更新用户最后访问
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="visitTime">访问时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        PartUserInfo GetPartUserById(int uid);

        /// <summary>
        /// 创建部分用户
        /// </summary>
        /// <param name="partUserInfo">部分用户信息</param>
        void CreatePartUser(PartUserInfo partUserInfo);

    }
}
