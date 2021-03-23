using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProduct.BusinessModel;
using WebAPIProduct.DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtEncodeController : ControllerBase
    {
        // GET: api/<JwtEncodeController>
        [HttpGet]
        public string Get()
        {
            //Just for test purpose and not sharing to this url to end user
            JWTToken objToken = new JWTToken();
            return objToken.encodeToken(ConString.GetSecretKey);
        }

    }
}
