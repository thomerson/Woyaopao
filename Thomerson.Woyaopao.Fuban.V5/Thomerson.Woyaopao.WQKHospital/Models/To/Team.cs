using System.Collections.Generic;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.WQKHospital
{
    public class Team
    {
        public string name { get; set; }
        /// <summary>
        /// 总里程数
        /// </summary>
        public double total { get; set; }

        /// <summary>
        /// 团队成员
        /// </summary>
        public List<Member> Members { get; set; }

        /// <summary>
        /// 团队总人数
        /// </summary>
        public int TotalMembers { get; set; }
    }
}
