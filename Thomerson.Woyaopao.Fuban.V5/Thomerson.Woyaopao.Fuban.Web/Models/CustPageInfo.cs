using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.Fuban
{
    public class CustPageInfo : DataTransfer
    {
        /// <summary>
        /// 目标总里程
        /// </summary>
        public double TagetTotal
        {
            get { return FubanPlatform.TotalTarget; }
        }

        public double CompleteTotal { get; set; }

        public double CompletePercent
        {
            get
            {
                return Math.Round(CompleteTotal * 100 / TagetTotal, 2);
            }
        }

        public List<MemberInfo> TopMembers { get; set; }

        public int AllTeamCount { get; set; }

    }
}