using System.Collections.Generic;

namespace captcha.Services.Db.Entity
{
    public class ApiKey
    {
        public int Id{get;set;}
        public string Key{get;set;}
        public List<SessionCaptcha> Sessions{get;set;}= new List<SessionCaptcha>();
    }
}