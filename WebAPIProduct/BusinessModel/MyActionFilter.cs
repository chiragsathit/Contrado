using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProduct.DataAccess;
using static WebAPIProduct.BusinessModel.CommonResponse;

namespace WebAPIProduct.BusinessModel
{
    public class MyActionFilter : IActionFilter
    {
        public JWTToken.ValidateTokenResponse ValidateTokenResult { get; set; }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = "";
            if(context.HttpContext.Request.Headers.ContainsKey("contrado_token"))
            {
                token = context.HttpContext.Request.Headers["contrado_token"].ToString();
            }
            JWTToken objToken = new JWTToken();
            if (token.Equals(""))
            {
                ValidateTokenResult = new JWTToken.ValidateTokenResponse() { isValid = false, error_message = "Token value required." };
            }
            else
            {
                ValidateTokenResult = objToken.ValidateJWTToken(token, ConString.GetSecretKey);
            }
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("");
        }
    }
}
