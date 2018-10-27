using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thomerson.Woyaopao.Core.Model;

namespace Thomerson.Woyaopao.Core.WQKHospital.To
{
    class DataForPage
    {
        public int TotalPerson { get; }
        public float TotalDistance { get; }
        public List<Team> Teams { get; set; }
        public List<MemberInfo> Male { get; set; }
        public List<MemberInfo> Female { get; set; }
    }
}
