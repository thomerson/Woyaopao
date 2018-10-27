using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.Model;

namespace Thomerson.Woyaopao.Fuban
{
    public class FubanPlatform : WoyaopaoConfig
    {
        public static int TotalTarget
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Woyaopao_Fuban_TotalTarget"]);
            }
        }

        public static sourceData GetDataFromSource()
        {
            var result = string.Empty;
            try
            {
                var client = new HttpClient();
                result = client.GetStringAsync(Woyaopao_DataUri).Result;

                result = ReplaceSpecialChar4Json(result);
                //Logger.Default.Info(result);
                var model = JsonConvert.DeserializeObject<sourceData>(result);
                return model;
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                Logger.Default.Info(result);
                return null;
            }

        }

        public static DataTransfer source2Transfer(sourceData source)
        {
            try
            {
                if (source.status != 1)
                {
                    Logger.Default.Info(string.Format("statue:{0};msg:{1}", source.status, source.msg));
                }
                var runinfo = source.data.runInfo;

                var userinfos = source.data.userInfo;

                var teaminfos = source.data.teamsInfo;

                var top100 = new List<MemberInfo>();

                var total = runinfo.Sum(s => s.distance);

                var order = runinfo.OrderByDescending(o => o.distance).ToList();

                var teams = teaminfos.ToDictionary(k => k.traminfoid, v => new Team()
                {
                    traminfoid = v.traminfoid,
                    name = v.teamname,
                    total = 0.0
                });

                for (int i = 0; i < order.Count(); i++)
                {
                    var user = GetPersonInfo(order[i].userid, userinfos);
                    if (user != null)
                    {
                        if (top100.Count < 100)
                        {
                            top100.Add(new MemberInfo()
                            {
                                //order = i + 1,
                                matchapplypeoplename = user?.matchapplypeoplename,
                                headpath = user?.headpath,
                                total = Math.Round(order[i].distance, 2)
                            });
                        }

                        if (!string.IsNullOrWhiteSpace(user.traminfoid) && teams.ContainsKey(user.traminfoid))
                        {
                            var team = teams[user.traminfoid];
                            team.total = team.total + order[i].distance;
                            teams[user.traminfoid] = team;
                        }
                    }
                }

                return new CustPageInfo()
                {
                    CompleteTotal = Math.Round(total, 0),
                    Teams = teams.Select(s => new Team() { traminfoid = s.Key, name = s.Value?.name, total = Math.Round(s.Value.total, 2) }).OrderByDescending(o => o.total).ToList(),
                    TopMembers = top100,
                    TotalMember = userinfos.Count,
                    AllTeamCount = teams.Count()
                };

            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                return null;
            }
        }



        public static void ServiceTimer()
        {

            //每30秒执行一次  
            var timespan = Woyaopao_source_Timespan;
            var t = new System.Timers.Timer(timespan);

            //设置是执行一次（false）还是一直执行(true)；  
            t.AutoReset = true;
            //是否执行System.Timers.Timer.Elapsed事件；  
            t.Enabled = true;
            //到达时间的时候执行事件(theout方法)；  
            t.Elapsed += new System.Timers.ElapsedEventHandler(Setsource);
        }

        private static void Setsource(object source, System.Timers.ElapsedEventArgs e)
        {
            Setsource();
        }

        public static void Setsource()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " start get source data");
                var source = GetDataFromSource();
                Console.WriteLine("geted");
                var entity = source2Transfer(source);
                Console.WriteLine("transfer");
                var json = JsonConvert.SerializeObject(entity);
                Console.WriteLine("write into redis");
                var sourceKey = Redis_sourceDataKey;
                AliRedisClient.getRedisConn().GetDatabase().StringSet(sourceKey, json);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " end");

                Console.WriteLine("*********************************");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Logger.Default.Error(ex);
            }
        }

        public static void GetsourceFromRedis()
        {
            try
            {
                var sourceKey = Redis_sourceDataKey;
                var result = AliRedisClient.getRedisConn().GetDatabase().StringGet(sourceKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Logger.Default.Error(ex);
            }
        }

        private static string ReplaceSpecialChar4Json(string json)
        {
            //if (json == null) { return json; }
            //if (json.Contains("\\"))
            //{
            //    json = json.Replace("\\", "\\\\");
            //}
            //if (json.Contains("\'"))
            //{
            //    json = json.Replace("\'", "\\\'");
            //}
            //if (json.Contains("\""))
            //{
            //    json = json.Replace("\"", "\\\"");
            //}
            ////去掉字符串的回车换行符
            //json = System.Text.RegularExpressions.Regex.Replace(json, @"[\n\r]", "");
            //json = json.Trim();
            return json;
        }

        private static CustUserInfo GetPersonInfo(string userid, List<CustUserInfo> userinfos)
        {
            var userKey = string.Format(Redis_UserId, userid);
            CustUserInfo person = null;

            var user = string.Empty;
            if (WoyaopaoConfig.UseRedis)
            {
                user = AliRedisClient.getRedisConn().GetDatabase().StringGet(userKey);
            }

            if (string.IsNullOrWhiteSpace(user))
            {
                var userinfo = userinfos.Where(s => s.userid == userid).FirstOrDefault();
                if (userinfo != null)
                {
                    person = userinfo;
                    if (WoyaopaoConfig.UseRedis)
                    {
                        AliRedisClient.getRedisConn().GetDatabase().StringSet(userKey, JsonConvert.SerializeObject(person));
                    }
                }
            }
            else
            {
                person = JsonConvert.DeserializeObject<CustUserInfo>(user);
            }
            return person;
        }
    }
}