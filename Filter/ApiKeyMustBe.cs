using captcha.Services.Repositoriy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace captcha.Filter
{
    public class ApiKeyMustBeFilterAttribute: TypeFilterAttribute
    {
        public ApiKeyMustBeFilterAttribute():base(typeof(ApiMustBeFilter))
        {
            
        }
        private class ApiMustBeFilter : IActionFilter
        {
            IRepositoriy _context;

            public ApiMustBeFilter(IRepositoriy context)
            {
                _context = context;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                string key = context.HttpContext.Request.Headers["Api-Key"];
                if(key==null)
                {
                    context.Result= new UnauthorizedObjectResult("Api Key not");
                }
                else if(!_context.ApiKeyIsExist(key))
                {
                    context.Result= new UnauthorizedObjectResult("Api Key invalid");
                }
            }
        }
    }
}