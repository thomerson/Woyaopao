using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.Model;
using Thomerson.Woyaopao.WQKHospital.Models;

namespace Thomerson.Woyaopao.WQKHospital
{
    public class WQKHospital : WoyaopaoConfig
    {
        private static Dictionary<string, Team> teamDict
        {
            get
            {
                var dict = new Dictionary<string, Team>();
                dict.Add("管理一部门工会", new Team() { memberCount = 0, total = 0.0, name = "管理一部门工会" });
                dict.Add("管理二部门工会", new Team() { memberCount = 0, total = 0.0, name = "管理二部门工会" });
                dict.Add("管理三部门工会", new Team() { memberCount = 0, total = 0.0, name = "管理三部门工会" });
                dict.Add("医技一部门工会", new Team() { memberCount = 0, total = 0.0, name = "医技一部门工会" });
                dict.Add("医技二部门工会", new Team() { memberCount = 0, total = 0.0, name = "医技二部门工会" });
                dict.Add("医技三部门工会", new Team() { memberCount = 0, total = 0.0, name = "医技三部门工会" });
                dict.Add("胸外科部门工会", new Team() { memberCount = 0, total = 0.0, name = "胸外科部门工会" });
                dict.Add("手术麻醉部门工会", new Team() { memberCount = 0, total = 0.0, name = "手术麻醉部门工会" });
                dict.Add("呼吸内科部门工会", new Team() { memberCount = 0, total = 0.0, name = "呼吸内科部门工会" });
                dict.Add("心内科部门工会", new Team() { memberCount = 0, total = 0.0, name = "心内科部门工会" });
                dict.Add("心外科部门工会", new Team() { memberCount = 0, total = 0.0, name = "心外科部门工会" });
                dict.Add("医学中心部门工会", new Team() { memberCount = 0, total = 0.0, name = "医学中心部门工会" });
                return dict;
            }
        }

        public static SourceData GetDataFromSource()
        {
            var result = string.Empty;
            try
            {
                var client = new HttpClient();
                result = client.GetStringAsync(sourceDataUri).Result;

                Logger.Default.Info(result);
                return JsonConvert.DeserializeObject<SourceData>(result);
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex);
                Logger.Default.Info(result);
                return null;
            }

        }

        public static Models.CustPageInfo DataTransfor(DataInfo data)
        {

            var runinfo = Filter(data.runInfo);

            var team = teamDict;
            var male = new List<Member>();
            var female = new List<Member>();
            var total = 0.0;

            //按userid分组 
            var group = runinfo.GroupBy(g => g.userid)
                .Select(s => new { UserId = s.Key, Total = s.Sum(t => t.distance) });
            //.OrderByDescending(o => o.Total);

            foreach (var item in group)
            {
                var user = GetPersonInfo(item.UserId, data.userInfo);
                if (user.gender == 0 && female.Count < 50) //女
                {
                    female.Add(new Member()
                    {
                        total = item.Total,
                        headpath = user.headpath,
                        nickname = user.nickname,
                        gender = user.gender
                    });
                }
                else if (user.gender == 1 && male.Count < 50) //男
                {
                    male.Add(new Member()
                    {
                        total = item.Total,
                        headpath = user.headpath,
                        nickname = user.nickname,
                        gender = user.gender
                    });
                }
                total += item.Total;

                if (team.ContainsKey(user.所属团队))
                {

                }
            }
            var result = new Models.CustPageInfo()
            {
                TotalInstance = Math.Round(total, 2),
                Male = male.OrderByDescending(o => o.total).ToList(),
                Female = female.OrderByDescending(o => o.total).ToList(),
                //Teams = team.Select(s => s.Value).OrderByDescending(o => o.total).ToList(),

            };
            return result;
        }

        public static CustUserInfo GetPersonInfo(string userid, List<CustUserInfo> userinfos)
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

        /// <summary>
        /// 无效数据过滤
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<RunInfo> Filter(List<RunInfo> list)
        {
            var result = new List<RunInfo>();
            foreach (var item in list)
            {
                // distance 在[2,10]为有效跑量
                if (item.distance >= 2)
                {
                    if (item.distance > 10)
                    {
                        item.distance = 10;
                    }
                    result.Add(item);
                }
            }
            return result;
        }

    }
}