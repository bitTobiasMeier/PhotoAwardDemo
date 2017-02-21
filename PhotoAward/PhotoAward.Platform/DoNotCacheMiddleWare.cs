using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Filters;
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
            context.Response.Headers["Expires"] = "-1";
            await Next.Invoke(context);
    }
  }



  [AttributeUsage(AttributeTargets.All)]
  public sealed class NoCacheHeaderFilter : ActionFilterAttribute
  {
    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    {
      if (actionExecutedContext?.Response == null)
        return;

      var response = actionExecutedContext.Response;
      response.Headers.CacheControl = new CacheControlHeaderValue
      {
        NoCache = true,
        NoStore = true,
        MustRevalidate = true
      };
      response.Headers.Pragma.Add(new NameValueHeaderValue("no-cache"));
      if (response.Content != null)
        response.Content.Headers.Expires = DateTimeOffset.UtcNow.AddDays(-1);
    }
  }
}