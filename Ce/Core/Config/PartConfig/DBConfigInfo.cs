using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [Serializable]
    public class DBConfigInfo 
    {
        private string _dbconnectionstring;
        private string _dbtablepre;

        public string DBConnectionString
        {
            get { return _dbconnectionstring; }
            set { _dbconnectionstring = value; }
        }

        public string DBTablePre
        {
            get { return _dbtablepre; }
            set { _dbtablepre = value; }
        }

    }
}
