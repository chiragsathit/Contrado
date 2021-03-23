using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebUIProduct.Models
{
    public static class APICall
    {
        public class APIData
        {
            public string message { get; set; }
            public bool isAPIResponse { get; set; }
            public string apiResponseString { get; set; }
        }
        public class CommomResponse
        {
            public int error_code { get; set; }
            public string message { get; set; }
        }
        public static APIData CallDeleteAPI(string url)
        {
            APIData objResponse = new APIData() { apiResponseString = "", message = "", isAPIResponse = false };
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url),
                        Headers = {
                                 { HttpRequestHeader.Accept.ToString(), "application/json" },
                                 { "contrado_token", CommonConfig.GetEncToken }
                                  }
                    };
                    HttpResponseMessage res = httpClient.Send(httpRequestMessage);
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        objResponse.apiResponseString = new StreamReader(res.Content.ReadAsStream()).ReadToEnd();
                        objResponse.isAPIResponse = true;
                        return objResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.Message;
                objResponse.isAPIResponse = false;
                return objResponse;
            }
            return objResponse;
        }
        public static APIData CallGETAPI(string url)
        {
            APIData objResponse = new APIData() { apiResponseString = "", message = "", isAPIResponse = false };
            try
            {

                using (var httpClient = new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url),
                        Headers = {
                                 { HttpRequestHeader.Accept.ToString(), "application/json" },
                                 { "contrado_token", CommonConfig.GetEncToken }
                                  }
                    };
                    HttpResponseMessage res = httpClient.Send(httpRequestMessage);
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        objResponse.apiResponseString = new StreamReader(res.Content.ReadAsStream()).ReadToEnd();
                        objResponse.isAPIResponse = true;
                        return objResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.Message;
                objResponse.isAPIResponse = false;
                return objResponse;
            }
            return objResponse;
        }
        public static APIData CallPOST_PUTAPI(string url,string data,HttpMethod objMethod)
        {
            APIData objResponse = new APIData() { apiResponseString = "", message = "", isAPIResponse = false };
            try
            {
                var post_data = new StringContent(data, Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = objMethod,
                        RequestUri = new Uri(url),
                        Headers = {
                                 { HttpRequestHeader.Accept.ToString(), "application/json" },
                                 { "contrado_token", CommonConfig.GetEncToken }
                                  },
                        Content = post_data
                    };
                    HttpResponseMessage res = httpClient.Send(httpRequestMessage);
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        objResponse.apiResponseString = new StreamReader(res.Content.ReadAsStream()).ReadToEnd();
                        objResponse.isAPIResponse = true;
                        return objResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.message = ex.Message;
                objResponse.isAPIResponse = false;
                return objResponse;
            }
            return objResponse;
        }
        
    }
}
