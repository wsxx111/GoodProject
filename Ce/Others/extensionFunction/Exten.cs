using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace extensionFunction
{
    static class Exten
    {
        public static int StringToInt(this string YuanString)
        {
            int id;
            int.TryParse(YuanString, out id);//这里当转换失败时返回的id为0
            return id;
        }

        public static int IntAddNum(this int yuanint, int AddNum)
        {
            return yuanint + AddNum;
        }
       
    }
}
