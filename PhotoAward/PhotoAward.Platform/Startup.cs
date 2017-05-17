using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Converters;
using Owin;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.Platform.Security;

namespace PhotoAward.Platform
{
    public static class Startup
    {
        public static void ConfigureFormatters(MediaTypeFormatterCollection formatters)
        {
            formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            formatters.JsonFormatter.SerializerSettings.Converters.Add(
                new IsoDateTimeConverter() {DateTimeFormat = "yyyy-MM-ddHH:mm:ss"});
        }

        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var config = new HttpConfiguration();
            UnityConfig.RegisterComponents(config);
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new NoCacheHeaderFilter());
            ConfigureFormatters(config.Formatters);

            var physicalFileSystem = new PhysicalFileSystem(@".\dist");
            var fileOptions = new FileServerOptions
            {
                EnableDefaultFiles = true,
                RequestPath = PathString.Empty,
                FileSystem = physicalFileSystem,
            };

            fileOptions.DefaultFilesOptions.DefaultFileNames = new[] {"index.html"};
            fileOptions.StaticFileOptions.FileSystem = fileOptions.FileSystem = physicalFileSystem;
            fileOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            fileOptions.EnableDirectoryBrowsing = true;

            var myProvider = new MemberAuthorizationServerProvider(new MemberManagementClientFactory());
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = myProvider
            };
            appBuilder.UseOAuthAuthorizationServer(options);
            appBuilder.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            appBuilder.UseWebApi(config);
            appBuilder.Use(typeof(DoNotCacheMiddleWare));
            appBuilder.UseFileServer(fileOptions);

        }

      
    }
}
