using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core.Model
{
    /// <summary>
    /// cache redis
    /// </summary>
    public class PersonInfoInRedis : PersonBase
    {
        public string teamInfoId { get; set; }
        public string teamName { get; set; }

    }

    public class PersonBase
    {
        public string headimg { get; set; }
        public string name { get; set; }
        public int gender { get; set; }
        //public string applypeoplename { get; set; }
    }

    /// <summary>
    /// show on page
    /// </summary>
    public class PersonShowOnPage : PersonBase
    {
        //public int order { get; set; }

        public double total { get; set; }
    }
}
