using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Token.Models
{
    public class LoginDataIn
    {
        public int WhichBussiness { get; set;}
        public string Token { get; set; }
        public string Username { get; set; }
        public string ClientTime { get; set; }
    }
    public class LoginData
    {
        public int Code { get; set; }
        public string Msg { get; set; }
    }
}