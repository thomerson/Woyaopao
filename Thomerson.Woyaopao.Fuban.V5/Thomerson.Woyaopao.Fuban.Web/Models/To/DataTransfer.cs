using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Model
{
    public class DataTransfer
    {
        public List<Team> Teams { get; set; }

        /// <summary>
        /// 活动总人数
        /// </summary>
        public int TotalMember { get; set; }

        public string sourceUrl
        {
            get
            {
                return Core.WoyaopaoConfig.sourceUrl;
            }
        }

        public string ApplyUrl
        {
            get
            {
                return Core.WoyaopaoConfig.ApplyUrl;
            }
        }


    }
}
