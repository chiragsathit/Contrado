using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProduct.BusinessModel
{
    public class CommonResponse
    {
        public int error_code { get; set; }
        public string message { get; set; }

        public enum ErrorCode
        {
            Success = 0,
            Failure = 1
        }
    }
}
