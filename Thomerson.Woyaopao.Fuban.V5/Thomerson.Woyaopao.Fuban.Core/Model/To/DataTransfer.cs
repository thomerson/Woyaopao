using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core.Model
{
    public class DataTransfer
    {
        public List<Team> Teams { get; set; }

        /// <summary>
        /// 活动总人数
        /// </summary>
        public int TotalMember { get; set; }

        public string SourseUrl
        {
            get
            {
                return WoyaopaoConfig.Woyaopao_SourseUrl;
            }
        }

        public string ApplyUrl
        {
            get
            {
                return WoyaopaoConfig.Woyaopao_ApplyUrl;
            }
        }


    }
}
