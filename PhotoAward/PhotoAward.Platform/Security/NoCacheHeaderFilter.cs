using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace PhotoAward.Platform.Security
{
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