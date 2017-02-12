using System.Threading.Tasks;
using Microsoft.Owin;

namespace PhotoAward.Platform
{
    public class DoNotCacheMiddleWare : OwinMiddleware
    {
        public DoNotCacheMiddleWare(OwinMiddleware next)
            : base(next)
        {
        }
        public override async Task Invoke(IOwinContext context)
        {
            context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            context.Response.Headers["Pragma"] = "no-cache";
            context.Response.Headers["Expires"] = "0";
            await Next.Invoke(context);
    }
  }
}