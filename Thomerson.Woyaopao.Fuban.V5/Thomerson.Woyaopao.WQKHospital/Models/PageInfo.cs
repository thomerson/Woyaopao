using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thomerson.Woyaopao.Core.Model;

namespace Thomerson.Woyaopao.WQKHospital.Models
{
    public class PageInfo : DataTransfer
    {
        /// <summary>
        /// 累计跑量
        /// </summary>
        public double TotalInstance { get; set; }

        /// <summary>
        /// 男子组  top50
        /// </summary>
        public List<MemberInfo> Male { get; set; }

        /// <summary>
        /// 女子组 top50
        /// </summary>
        public List<MemberInfo> Female { get; set; }

    }
}