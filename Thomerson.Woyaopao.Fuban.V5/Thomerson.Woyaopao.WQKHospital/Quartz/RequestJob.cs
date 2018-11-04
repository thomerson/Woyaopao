using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Core
{
    public class RequestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Logger.Default.Info(string.Format("请求时间:{0}", DateTime.Now));
                var source = Woyaopao.WQKHospital.WQKHospital.GetDataFromSource();
                var entity = WQKHospital.WQKHospital.DataTransfor(source.data);
                var json = JsonConvert.SerializeObject(entity);
                HttpRuntimeCache.SetCache(WoyaopaoConfig.Redis_sourceDataKey, json, WoyaopaoConfig.Redis_Overtime);
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
            }
        }
    }
}
