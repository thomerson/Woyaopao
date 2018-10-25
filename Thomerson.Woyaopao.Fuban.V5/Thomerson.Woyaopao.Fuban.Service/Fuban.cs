using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Thomerson.Woyaopao.Core;

namespace Thomerson.Woyaopao.Fuban.Service
{
    public partial class Fuban : ServiceBase
    {

        //定时器  
        System.Timers.Timer t = null;


        public Fuban()
        {
            InitializeComponent();

            //启用暂停恢复  
            base.CanPauseAndContinue = true;

            //每30秒执行一次  
            var timespan = WoyaopaoConfig.Woyaopao_Fuban_Sourse_Timespan;
            t = new System.Timers.Timer(timespan);

            //设置是执行一次（false）还是一直执行(true)；  
            t.AutoReset = true;
            //是否执行System.Timers.Timer.Elapsed事件；  
            t.Enabled = true;
            //到达时间的时候执行事件(theout方法)；  
            t.Elapsed += new System.Timers.ElapsedEventHandler(SetSourse);

        }

        protected override void OnStart(string[] args)
        {
            var state = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "启动";
            Logger.Default.Info(state);
        }

        protected override void OnStop()
        {
            var state = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "停止";
            Logger.Default.Info(state);
        }

        //恢复服务执行  
        protected override void OnContinue()
        {
            var state = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "继续";
            Logger.Default.Info(state);
            t.Start();
        }

        protected override void OnPause()
        {
            var state = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "暂停";
            Logger.Default.Info(state);
            t.Stop();
        }

        private void SetSourse(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var sourse = WoyaopaoFuban.GetDataFromSource();

                var entity = WoyaopaoFuban.Sourse2Transfer(sourse);

                var json = JsonConvert.SerializeObject(entity);

                var sourseKey = WoyaopaoConfig.Redis_Fuban_SourseDataKey;
                AliRedisClient.getRedisConn().GetDatabase().StringSet(sourseKey, json);

            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
            }
        }
    }
}
