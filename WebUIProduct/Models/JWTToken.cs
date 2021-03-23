using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;


namespace WebUIProduct.Models
{
  
    public class JWTToken
    {
        public static string encodeToken(string secret_key)
        {
            try
            {
                DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
                var my_jsondata = new Dictionary<string, string>
            {
                { "TimeStamp", now.ToUnixTimeSeconds().ToString() }
            };
                //Tranform it to Json object
                string json_data = JsonConvert.SerializeObject(my_jsondata);
                JObject json_object = JObject.Parse(json_data);

                var algorithm = new HMACSHA256Algorithm();
                var urlEncoder = new JwtBase64UrlEncoder();
                var serializer = new JsonNetSerializer();
                var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

                string token = encoder.Encode(json_object, secret_key);
                return token;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}
