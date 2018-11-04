using System.Collections.Generic;
using System.Linq;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.WQKHospital.Models
{
    public class CustPageInfo : PageInfo
    {
        /// <summary>
        /// 男子组  top50
        /// </summary>
        public List<Member> Male { get; set; }

        /// <summary>
        /// 女子组 top50
        /// </summary>
        public List<Member> Female { get; set; }

        public List<Team> Teams { get; set; }
        /// <summary>
        /// 前三
        /// </summary>
        public List<Team> Top3
        {
            get
            {
                return Teams?.Take(3).ToList();
            }
        }

        /// <summary>
        /// 活动总人数
        /// </summary>
        public int TotalMember { get; set; }

    }
}