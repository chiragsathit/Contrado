using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebUIProduct.Models
{
    public class CommonConfig
    {
        public static string GetAPIEndPoint
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false);
                return configurationBuilder.Build().GetSection("APIEndPoint:BaseURL").Value;

            }
        }
        public static string GetSecretKey
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false);
                return configurationBuilder.Build().GetSection("AccessKey:secret_key").Value;

            }
        }
        public static string GetEncToken
        {
            get
            {
               return JWTToken.encodeToken(GetSecretKey);
            }
        }
    }
}
