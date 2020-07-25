using System;
using System.Linq;
using captcha.Services.Db;
using captcha.Services.Db.Entity;
using captcha.Services.DrawCaptcha;

namespace captcha.Services.Repositoriy
{
    public class Repositoriy:IRepositoriy
    {
        private Context _context;

        public Repositoriy(Context context)
        {
            _context = context;
        }

        public bool sessionCodeIsEqual(string sessionKey,string code,string apikey)
        {
            return _context.SessionCaptchas.Any(x=>x.SessionKey==sessionKey&&x.Code==code);
        }

        public void setSessionStatusTrue(string sessionKey,string apikey)
        {
            var sess =_context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey&&x.ApiKey.Key==apikey);
            if(sess==null){
                return;
            }
            sess.Status=true;
            _context.SessionCaptchas.Update(sess);
            _context.SaveChanges();

        }

        public string createSession(Captcha captcha,string apikey)
        {
            var sessionKey=Guid.NewGuid().ToString();
            var apiKey= _context.ApiKeys.FirstOrDefault(x=>x.Key==apikey);
            var session= new SessionCaptcha()
                {Code=captcha.Key, SessionKey=sessionKey, Status=false,ApiKey=apiKey};

            
            _context.SessionCaptchas.Add(session);
            apiKey.Sessions.Add(session);
            _context.ApiKeys.Update(apiKey);
            _context.SaveChanges();
            return sessionKey;
        }

        public bool checkSessionIsExpired(string sessionKey,string apikey)
        {
            var session= _context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey&&x.ApiKey.Key==apikey);
            return session.TimeExpires<DateTime.Now;
        }

        public void deleteSession(string sessionKey,string apikey)
        {
            var session=  _context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey&&x.ApiKey.Key==apikey);
            if(session==null) return;
            _context.Remove(session);
            _context.SaveChanges();
        }

        public bool? getSessionStatus(string sessionKey,string apikey)
        {
            var session=  _context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey&&x.ApiKey.Key==apikey);
            if(session==null) return null;
            return session.Status;
        }

        public ApiKey createApiKey()
        {
            
            var key= Guid.NewGuid().ToString();
            var ApiKey= new ApiKey(){Key=key};

            _context.ApiKeys.Add(ApiKey);
            _context.SaveChanges();
            return ApiKey;
        }

        public bool ApiKeyIsExist(string apikey)
        {
            return _context.ApiKeys.Any(x=>x.Key==apikey);
        }

        public bool ApiKeyExistSession(string apikey, string session)
        {
            return _context.SessionCaptchas.Any(x=>x.SessionKey==session&&x.ApiKey.Key==session);
        }
    }
}