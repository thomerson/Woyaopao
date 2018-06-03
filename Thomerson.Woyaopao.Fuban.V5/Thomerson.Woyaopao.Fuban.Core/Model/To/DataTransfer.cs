using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Fuban.Core.Model
{
    public class DataTransfer
    {
        public double TagetTotal
        {
            get { return WoyaopaoConfig.Woyaopao_Fuban_TotalTarget; }
        }

        public double CompleteTotal { get; set; }

        public double CompletePercent
        {
            get
            {
                return Math.Round(CompleteTotal * 100 / TagetTotal, 2);
            }
        }

        public List<Team> Teams { get; set; }

        public List<PersonShowOnPage> TopMembers { get; set; }

        public int AllMemberCount { get; set; }

        public string SourseUrl
        {
            get
            {
                return WoyaopaoConfig.Woyaopao_Fuban_SourseUrl;
            }
        }

        public string ApplyUrl
        {
            get
            {
                return WoyaopaoConfig.Woyaopao_Fuban_ApplyUrl;
            }
        }

        public int AllTeamCount { get; set; }

    }
}
