using Senparc.Weixin.MP.Entities;
using System;

namespace Thomerson.Wechat.JSSDK.Models
{
    public class LocalAccessToken : AccessTokenResult
    {
        public DateTime? ExpiresTime { get; set; }

        public LocalAccessToken(AccessTokenResult result)
        {
            access_token = result.access_token;
            expires_in = result.expires_in;
            errcode = result.errcode;
            ExpiresTime = DateTime.Now.AddSeconds(result.expires_in - 10);
        }
    }
}