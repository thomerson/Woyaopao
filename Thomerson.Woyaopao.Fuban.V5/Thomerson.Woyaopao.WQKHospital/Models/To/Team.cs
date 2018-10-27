using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.WQKHospital
{
    public class Team
    {
        public string name { get; set; }
        /// <summary>
        /// 团队人数
        /// </summary>
        public int memberCount { get; set; }
        /// <summary>
        /// 总里程数
        /// </summary>
        public double total { get; set; }
    }
}
