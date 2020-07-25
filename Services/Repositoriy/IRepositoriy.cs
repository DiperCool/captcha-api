using captcha.Services.Db.Entity;
using captcha.Services.DrawCaptcha;

namespace captcha.Services.Repositoriy
{
    public interface IRepositoriy
    {
        string createSession(Captcha captcha,string key);
        bool sessionCodeIsEqual(string sessionKey,string code,string key);
        void setSessionStatusTrue(string sessionKey,string key);
        bool checkSessionIsExpired(string sessionKey,string key);
        void deleteSession(string sessionKey,string key);
        bool? getSessionStatus(string sessionKey,string key);
        ApiKey createApiKey();
        bool ApiKeyIsExist(string apikey);
        bool ApiKeyExistSession(string apikey, string session);
    }
}