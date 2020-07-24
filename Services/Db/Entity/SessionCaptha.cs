using System;

namespace captcha.Services.Db.Entity
{
    public class SessionCaptcha
    {
        public int Id{get;set;}
        public string SessionKey{get;set;}
        public string Code{get;set;}
        public bool Status{get;set;}
        public DateTime TimeExpires{get;set;}= DateTime.Now.AddMinutes(5);
    }
}