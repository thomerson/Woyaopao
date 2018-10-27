using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thomerson.Woyaopao.Core;

namespace Thomerson.Woyaopao.Model
{
    public class PageInfo
    {
        /// <summary>
        /// 活动总人数
        /// </summary>
        public int TotalMember { get; set; }

        public string sourceUrl
        {
            get
            {
                return WoyaopaoConfig.sourceUrl;
            }
        }

        public string ApplyUrl
        {
            get
            {
                return WoyaopaoConfig.ApplyUrl;
            }
        }

    }
}
