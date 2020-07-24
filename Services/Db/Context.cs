using captcha.Services.Db.Entity;
using Microsoft.EntityFrameworkCore;

namespace captcha.Services.Db
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
        }

        public DbSet<SessionCaptcha> SessionCaptchas{get;set;}
    }
}