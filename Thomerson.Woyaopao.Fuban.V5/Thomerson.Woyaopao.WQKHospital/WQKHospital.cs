using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Thomerson.Woyaopao.Core;
using Thomerson.Woyaopao.Model;
using Thomerson.Woyaopao.WQKHospital.Models;

namespace Thomerson.Woyaopao.WQKHospital
{
    public class WQKHospital : WoyaopaoConfig
    {
        private static List<string> TeamList = new List<string>() {
                "管理一部门工会",
                "管理二部门工会",
                "管理三部门工会",
                "医技一部门工会",
                "医技二部门工会",
                "医技三部门工会",
                "胸外科部门工会",
                "手术麻醉部门工会",
                "呼吸内科部门工会",
                "心内科部门工会",
                "心外科部门工会",
                "医学中心部门工会"
                };

        public static SourceData GetDataFromSource()
        {
            var result = string.Empty;
            try
            {
                var client = new HttpClient();
                result = client.GetStringAsync(sourceDataUri).Result;

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

            var team = TeamList.ToDictionary(k => k, v => new Team()
            {
                name = v,
                Members = new List<Member>(),
                total = 0.0,
                TotalMembers = data.userInfo?.Where(w => w.团队名称 == v).Count() ?? 0
            });
            var male = new List<Member>();
            var female = new List<Member>();
            var total = 0.0;

            //按userid分组 
            var group = runinfo.GroupBy(g => g.userid)
                .Select(s => new { UserId = s.Key, Total = s.Sum(t => t.distance) })
                .OrderByDescending(o => o.Total);

            foreach (var item in group)
            {
                var user = GetPersonInfo(item.UserId, data.userInfo);
                var member = new Member()
                {
                    total = item.Total,
                    headpath = user.headpath,
                    nickname = user.applypeoplename,
                    gender = user.gender,
                    userid = user.userid
                };
                if (user.gender == 0 && female.Count < 50) //女 top50
                {
                    female.Add(member);
                }
                else if (user.gender == 1 && male.Count < 50) //男  top50
                {
                    male.Add(member);
                }
                total += item.Total;

                if (!string.IsNullOrWhiteSpace(user.团队名称) && team.ContainsKey(user.团队名称))
                {
                    team[user.团队名称].Members.Add(member);
                    team[user.团队名称].total += item.Total;
                }
            }
            var result = new Models.CustPageInfo()
            {
                TotalInstance = Math.Round(total, 2),
                Male = male.OrderByDescending(o => o.total).ToList(),
                Female = female.OrderByDescending(o => o.total).ToList(),
                Teams = team.Select(s => s.Value).OrderByDescending(o => o.total).ToList(),
                TotalMember = data.userInfo.Count
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
                //user = AliRedisClient.getRedisConn().GetDatabase().StringGet(userKey);
                user = HttpRuntimeCache.GetCache(userKey) as string;
            }

            if (string.IsNullOrWhiteSpace(user))
            {
                var userinfo = userinfos.Where(s => s.userid == userid).FirstOrDefault();
                if (userinfo != null)
                {
                    person = userinfo;
                    if (WoyaopaoConfig.UseRedis)
                    {
                        HttpRuntimeCache.SetCache(userKey, JsonConvert.SerializeObject(person), WoyaopaoConfig.Redis_Overtime);
                        //AliRedisClient.getRedisConn().GetDatabase().StringSet(userKey, JsonConvert.SerializeObject(person));
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

            //var temp = list.GroupBy(g => new { g.userid, g.runtime }).Select(s => new RunInfo() { })

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