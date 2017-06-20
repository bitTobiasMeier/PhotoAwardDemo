using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

namespace PhotoAward.Platform.Security
{
    public class FileRewriterMiddleware : OwinMiddleware
    {
        private readonly FileServerOptions _options;

        public FileRewriterMiddleware(OwinMiddleware next, FileServerOptions options) : base(next)
        {
            _options = options;
        }

        /// <summary>
        /// Process an individual request.
        /// </summary>
        /// <param name="context"/>
        /// <returns/>
        public override async Task Invoke(IOwinContext context)
        {
            if (context.Response.StatusCode != 404)
            {
                await Next.Invoke(context);
                ;
            }

            if (context.Response.StatusCode != 404)
            {
                return;
            }

            var middleware = new StaticFileMiddleware(
                env => Next.Invoke(context), new StaticFileOptions
                {
                    FileSystem = new PhysicalFileSystem("./dist"),
                    RequestPath = PathString.Empty
                });

            context.Request.Path = new PathString("/index.html");
            await middleware.Invoke(context.Environment);

        }


    }
}
