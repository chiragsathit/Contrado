using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProduct.DapperORM
{
    public class ProductAttributeLookupTable
    {
        public int AttributeId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number")]
        public int ProdCatId { get; set; }
        [Required]
        [StringLength(250)]
        public string AttributeName { get; set; }

    }
    public class ProductAttributeLookupInsertParam
    {
        public int ProdCatId { get; set; }
        public string AttributeName { get; set; }
    }
    public class ProductAttributeLookupUpdateParam
    {
        public int AttributeId { get; set; }
        public int ProdCatId { get; set; }
        public string AttributeName { get; set; }
    }
}
