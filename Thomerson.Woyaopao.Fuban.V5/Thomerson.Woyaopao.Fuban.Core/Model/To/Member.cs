using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.Model
{
    public class Member : UserInfo
    {
        public double total { get; set; }
        public string teamname { get; set; }
    }
}
