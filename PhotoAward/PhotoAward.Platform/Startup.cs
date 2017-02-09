using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using Owin;

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
            var config = new HttpConfiguration();
            UnityConfig.RegisterComponents(config);
            config.MapHttpAttributeRoutes();
            ConfigureFormatters(config.Formatters);
            appBuilder.UseWebApi(config);
        }
    }
}
