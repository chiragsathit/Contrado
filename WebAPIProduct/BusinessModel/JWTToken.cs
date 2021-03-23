using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPIProduct.BusinessModel
{
    public class JWTToken
    {
        public class ValidateTokenResponse
        {
            public bool isValid { get; set; }
            public string error_message { get; set; }
        }


        public string encodeToken(string secret_key)
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
        public string decodeToken(string token, string secret_key)
        {
            try
            {
                var algorithm = new HMACSHA256Algorithm();
                var urlEncoder = new JwtBase64UrlEncoder();
                var serializer = new JsonNetSerializer();
                var dateTimeProvider = new UtcDateTimeProvider();
                var jwtvalidate = new JwtValidator(serializer, dateTimeProvider);
                var decoder = new JwtDecoder(serializer, jwtvalidate, urlEncoder, algorithm);
                return decoder.Decode(token, secret_key, true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unexpected character encountered while parsing value"))
                {
                    throw new Exception("Invalid JWT token format.");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public ValidateTokenResponse ValidateJWTToken(string tokenString, string secret_key)
        {
            try
            {
                if (secret_key.Length == 0)
                {
                    return new ValidateTokenResponse() { isValid = false, error_message = "Invalid secret key." };
                }
                string tokenJsonstring = decodeToken(tokenString, secret_key);
                JObject objToken = JObject.Parse(tokenJsonstring);
                IEnumerable<JToken> TimeStampClaim = objToken.Children().Where(obj => obj.Path.Equals("TimeStamp"));
                if (!TimeStampClaim.Any())
                {
                    return new ValidateTokenResponse() { isValid = false, error_message = "TimeStamp parameter is required."};
                }
                DateTimeOffset now = DateTime.UtcNow;
                if (now.ToUnixTimeSeconds() - Convert.ToInt64(TimeStampClaim.ToList()[0].Values<string>().ToList()[0]) > 300)
                {
                    return new ValidateTokenResponse() { isValid = false, error_message = "Timestamp is expired." };
                }
                return new ValidateTokenResponse() { isValid = true, error_message = "" };
            }
            catch (Exception ex)
            {
                return new ValidateTokenResponse() { isValid = false, error_message= ex.Message};
            }


        }

    }
}
