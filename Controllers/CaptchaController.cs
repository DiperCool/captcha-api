using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using captcha.Filter;
using captcha.Models;
using captcha.Services.Db;
using captcha.Services.DrawCaptcha;
using captcha.Services.Repositoriy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace captcha.Controllers
{
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private IRepositoriy _context;
        private IDrawCaptcha _draw;

        public CaptchaController(IRepositoriy context, IDrawCaptcha draw)
        {
            _context = context;
            _draw = draw;
        }

        [HttpPost("/create/session")]
        [ApiKeyMustBeFilter]
        public IActionResult createSession()
        {
            Captcha captcha= _draw.DrawsCaptcha();
            var sessionKey=_context.createSession(captcha, HttpContext.Request.Headers["Api-Key"]);
            return Ok(new CaptchaDTO(){SessionKey=sessionKey, Image=captcha.Image});
        }
        [HttpPost("/set/session")]
        [ApiKeyMustBeFilter]
        public IActionResult setStatus(string sessionKey, string code)
        {
            var res= _context.sessionCodeIsEqual(sessionKey, code,HttpContext.Request.Headers["Api-Key"]);
            if(!res) return BadRequest("Code is invalid");
            _context.setSessionStatusTrue(sessionKey,HttpContext.Request.Headers["Api-Key"]);
            return Ok();
        }

        [HttpGet("/get/session")]
        [ApiKeyMustBeFilter]
        public IActionResult getStatus(string sessionKey)
        {
            var res= _context.getSessionStatus(sessionKey,HttpContext.Request.Headers["Api-Key"]);
            if(res==null) return Ok("session not exist");
            return Ok(res);
        }
        [HttpPost("/create/apikey")]
        public IActionResult createApiKey()
        {
            return Ok(_context.createApiKey().Key);
        }

    }
}
