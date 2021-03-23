using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProduct.DapperORM;

namespace WebAPIProduct.BusinessModel
{
    public class ProductAttributeResponse
    {
        public class SelectData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public List<ProductAttributeTable> data { get; set; }
        }
        public class InsertData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public int RowEffect { get; set; }
        }
        public class DeleteData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public int RowEffect { get; set; }
        }
        public class UpdateData
        {
            public int error_code { get; set; }
            public string message { get; set; }
            public int RowEffect { get; set; }
        }

    }
}
