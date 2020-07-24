using captcha.Services.DrawCaptcha;

namespace captcha.Services.Repositoriy
{
    public interface IRepositoriy
    {
        string createSession(Captcha captcha);
        bool sessionCodeIsEqual(string sessionKey,string code);
        void setSessionStatusTrue(string sessionKey);
        bool checkSessionIsExpired(string sessionKey);
        void deleteSession(string sessionKey);
        bool? getSessionStatus(string sessionKey);
    }
}