using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core.Model
{
    public class DataInfo
    {
        public List<RunInfo> runInfo { get; set; }
        public List<UserInfo> userInfo { get; set; }
        public List<TeamsInfo> teamsInfo { get; set; }
    }
}
