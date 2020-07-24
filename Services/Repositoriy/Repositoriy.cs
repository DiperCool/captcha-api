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

        public bool sessionCodeIsEqual(string sessionKey,string code)
        {
            return _context.SessionCaptchas.Any(x=>x.SessionKey==sessionKey&&x.Code==code);
        }

        public void setSessionStatusTrue(string sessionKey)
        {
            var sess =_context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey);
            if(sess==null){
                return;
            }
            sess.Status=true;
            _context.SessionCaptchas.Update(sess);
            _context.SaveChanges();

        }

        public string createSession(Captcha captcha)
        {
            var sessionKey=Guid.NewGuid().ToString();
            var session= new SessionCaptcha()
                {Code=captcha.Key, SessionKey=sessionKey, Status=false};
            _context.SessionCaptchas.Add(session);
            _context.SaveChanges();
            return sessionKey;
        }

        public bool checkSessionIsExpired(string sessionKey)
        {
            var session= _context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey);
            return session.TimeExpires<DateTime.Now;
        }

        public void deleteSession(string sessionKey)
        {
            var session=  _context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey);
            if(session==null) return;
            _context.Remove(session);
            _context.SaveChanges();
        }

        public bool? getSessionStatus(string sessionKey)
        {
            var session=  _context.SessionCaptchas.FirstOrDefault(x=>x.SessionKey==sessionKey);
            if(session==null) return null;
            return session.Status;
        }
    }
}