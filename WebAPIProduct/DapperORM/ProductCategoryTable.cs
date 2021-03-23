using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProduct.DapperORM
{
    public class ProductCategoryTable
    {
        public int ProdCatId { get; set; }
        [Required]
        [StringLength(250)]
        [MinLength(3,ErrorMessage ="Minimum three character required in product category")]
        public string CategoryName { get; set; }
    }
    public class ProductCategoryInsertParam
    {
        public string CategoryName { get; set; }
    }
    public class ProductCategoryUpdateParam
    {
        public int ProdCatId { get; set; }
        public string CategoryName { get; set; }
    }
   
}
