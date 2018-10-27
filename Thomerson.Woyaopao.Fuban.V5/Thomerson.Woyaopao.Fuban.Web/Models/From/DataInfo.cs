using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Model
{
    public class DataInfo
    {
        public List<CustRunInfo> runInfo { get; set; }
        public List<CustUserInfo> userInfo { get; set; }
        public List<CustTeamInfo> teamsInfo { get; set; }
    }
}
