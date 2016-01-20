using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Token.Models
{
    public class TokenEntity
    {
        public string TokenCode { get; set; }
        public int  LeftTime { get; set; }
    }
}