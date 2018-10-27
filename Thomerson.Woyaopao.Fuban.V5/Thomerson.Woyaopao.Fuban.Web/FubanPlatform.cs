using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.Core.Model;

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

        public static SourseData GetDataFromSource()
        {
            var result = string.Empty;
            try
            {
                var client = new HttpClient();
                result = client.GetStringAsync(Woyaopao_DataUri).Result;

                result = ReplaceSpecialChar4Json(result);
                //Logger.Default.Info(result);
                var model = JsonConvert.DeserializeObject<SourseData>(result);
                return model;
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                Logger.Default.Info(result);
                return null;
            }

        }

        public static DataTransfer Sourse2Transfer(SourseData sourse)
        {
            try
            {
                if (sourse.status != 1)
                {
                    Logger.Default.Info(string.Format("statue:{0};msg:{1}", sourse.status, sourse.msg));
                }
                var runinfo = sourse.data.runInfo;

                var userinfos = sourse.data.userInfo;

                var teaminfos = sourse.data.teamsInfo;

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
                                name = user?.name,
                                headimg = user?.headimg,
                                total = Math.Round(order[i].distance, 2)
                            });
                        }

                        if (!string.IsNullOrWhiteSpace(user.teamInfoId) && teams.ContainsKey(user.teamInfoId))
                        {
                            var team = teams[user.teamInfoId];
                            team.total = team.total + order[i].distance;
                            teams[user.teamInfoId] = team;
                        }
                    }
                }

                return new PageInfo()
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

        public static PersonInfoInRedis GetPersonInfo(string userid, List<UserInfo> userinfos)
        {
            var userKey = string.Format(Redis_UserId, userid);
            PersonInfoInRedis person = null;

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
                    person = new PersonInfoInRedis()
                    {
                        headimg = userinfo.headpath,
                        name = userinfo.nickname,
                        gender = userinfo.gender,
                        teamInfoId = userinfo.traminfoid
                    };
                    if (WoyaopaoConfig.UseRedis)
                    {
                        AliRedisClient.getRedisConn().GetDatabase().StringSet(userKey, JsonConvert.SerializeObject(person));
                    }
                }
            }
            else
            {
                person = JsonConvert.DeserializeObject<PersonInfoInRedis>(user);
            }
            return person;
        }

        public static void ServiceTimer()
        {

            //每30秒执行一次  
            var timespan = Woyaopao_Sourse_Timespan;
            var t = new System.Timers.Timer(timespan);

            //设置是执行一次（false）还是一直执行(true)；  
            t.AutoReset = true;
            //是否执行System.Timers.Timer.Elapsed事件；  
            t.Enabled = true;
            //到达时间的时候执行事件(theout方法)；  
            t.Elapsed += new System.Timers.ElapsedEventHandler(SetSourse);
        }

        private static void SetSourse(object source, System.Timers.ElapsedEventArgs e)
        {
            SetSourse();
        }

        public static void SetSourse()
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " start get sourse data");
                var sourse = GetDataFromSource();
                Console.WriteLine("geted");
                var entity = Sourse2Transfer(sourse);
                Console.WriteLine("transfer");
                var json = JsonConvert.SerializeObject(entity);
                Console.WriteLine("write into redis");
                var sourseKey = Redis_SourseDataKey;
                AliRedisClient.getRedisConn().GetDatabase().StringSet(sourseKey, json);
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " end");

                Console.WriteLine("*********************************");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Logger.Default.Error(ex);
            }
        }

        public static void GetSourseFromRedis()
        {
            try
            {
                var sourseKey = Redis_SourseDataKey;
                var result = AliRedisClient.getRedisConn().GetDatabase().StringGet(sourseKey);
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
    }
}