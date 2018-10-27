using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.WQKHospital.Models
{
    public class DataInfo
    {
        public List<RunInfo> runInfo { get; set; }
        public List<CustUserInfo> userInfo { get; set; }
    }
}