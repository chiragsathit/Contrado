using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProduct.DataAccess
{
    public class ConString
    {
        public static string GetConnectionString
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false);
                return configurationBuilder.Build().GetSection("ConnectionStrings:ProductDatabase").Value;

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

        
    }
}
