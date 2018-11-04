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
        /// 总里程
        /// </summary>
        public double TotalInstance { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string SourceUrl
        {
            get
            {
                return WoyaopaoConfig.sourceUrl;
            }
        }

        /// <summary>
        /// 报名地址
        /// </summary>
        public string ApplyUrl
        {
            get
            {
                return WoyaopaoConfig.ApplyUrl;
            }
        }

    }
}
