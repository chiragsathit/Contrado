using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProduct.DapperORM
{
    public class ProductTable
    {
        [Range(0, long.MaxValue, ErrorMessage = "Please enter valid number")]
        public long ProductId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number")]
        public int ProdCatId { get; set; }
        [Required]
        [StringLength(250)]
        [MinLength(3, ErrorMessage = "Minimum 3 character required in product name.")]
        public string ProdName { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Minimum 3 character required in product description.")]
        public string ProdDescription { get; set; }
        public string CategoryName { get; set; }
    }
    public class ProductInsertParam
    {
        public int ProdCatId { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
    }
    public class ProductUpdateParam
    {
        public long ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
    }
}
