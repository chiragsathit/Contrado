using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace WebAPIProduct.DapperORM
{
    public class ProductAttributeTable
    {
        [Range(0, long.MaxValue, ErrorMessage = "Please enter valid number")]
        public long ProductId { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid number")]
        public int AttributeId { get; set; }
        [Required]
        [StringLength(250)]
        public string AttributeValue { get; set; }
    }
    public class ProductAttributeInsertParam
    {
        public long ProductId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
    public class ProductAttributeUpdateParam
    {
        public long ProductId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
    }
    public class ProductAttributeDeleteParam
    {
        public long ProductId { get; set; }
        public int AttributeId { get; set; }
    }
}
