using captcha.Filter;
using captcha.Models;
using captcha.Services.DrawCaptcha;
using captcha.Services.Repositoriy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace captcha.Controllers
{
    [ApiController]
    public class GenerateApiKeyController: ControllerBase
    {
        private IRepositoriy _context;
        private IDrawCaptcha _draw;
        private IConfiguration _configuration;

        public GenerateApiKeyController(IRepositoriy context, IDrawCaptcha draw,IConfiguration configuration)
        {
            _context = context;
            _draw = draw;
            _configuration=configuration;
        }
        [HttpPost("/generateCaptcha")]
        public IActionResult GenerateCaptcha()
        {
            var captcha= _draw.DrawsCaptcha();
            var res=_context.createSession(captcha, _configuration["Keys:Api-Key"]);
            return Ok(new CaptchaDTO(){SessionKey=res, Image=captcha.Image});
        }

        [HttpPost("/create/apikey")]
        [CheckCaptchaFilter]
        public IActionResult createApiKey()
        {
            return Ok(_context.createApiKey().Key);
        }
    }
}