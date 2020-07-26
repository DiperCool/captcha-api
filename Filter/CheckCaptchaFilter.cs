using captcha.Services.Repositoriy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace captcha.Filter
{
    public class CheckCaptchaFilterAttribute:TypeFilterAttribute
    {
        public CheckCaptchaFilterAttribute():base(typeof(CheckCaptchaFilter))
        {
            
        }

        private class CheckCaptchaFilter : IActionFilter
        {
            private IRepositoriy _context;
            private string ApiKey;

            public CheckCaptchaFilter(IRepositoriy context,IConfiguration configuration)
            {
                _context = context;
                ApiKey=configuration["Keys:Api-Key"];
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                string session= context.HttpContext.Request.Headers["SessionCaptcha"];
                string code= context.HttpContext.Request.Headers["CodeCaptcha"];
                if(session==null||code==null)
                {
                    context.Result= new BadRequestResult();
                }
                var result= _context.sessionCodeIsEqual(session, code, ApiKey);
                _context.setSessionStatus(session, ApiKey, result);
                if(!result){
                    context.Result= new BadRequestObjectResult("invalid code");
                }

                


            }
        }
    }
}