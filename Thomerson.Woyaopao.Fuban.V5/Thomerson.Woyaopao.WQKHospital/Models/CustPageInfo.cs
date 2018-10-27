using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.WQKHospital.Models
{
    public class CustPageInfo : PageInfo
    {
        /// <summary>
        /// 累计跑量
        /// </summary>
        public double TotalInstance { get; set; }

        /// <summary>
        /// 男子组  top50
        /// </summary>
        public List<Member> Male { get; set; }

        /// <summary>
        /// 女子组 top50
        /// </summary>
        public List<Member> Female { get; set; }

    }
}