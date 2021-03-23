using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProduct.BusinessModel;
using WebAPIProduct.DapperORM;

namespace WebAPIProduct.DataAccess
{
    public sealed class ProductAttributeDBAccess
    {

        private static readonly Lazy<ProductAttributeDBAccess> instance = new Lazy<ProductAttributeDBAccess>(() => new ProductAttributeDBAccess());

        public static ProductAttributeDBAccess getInstance
        {
            get
            {
                return instance.Value;
            }
        }
        public List<ProductAttributeTable> GetALLProductAttribute()
        {
            List<ProductAttributeTable> getProductList = new List<ProductAttributeTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductAttributeTable>("SELECT * FROM TblProductAttributeView").ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductAttributeTable> GetProductAttributeByProductId(int AttributeId)
        {
            List<ProductAttributeTable> getProductList = new List<ProductAttributeTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductAttributeTable>("SELECT * FROM TblProductAttributeView WHERE ProductId=" + AttributeId).ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductAttributeTable> GetProductAttributeByAttributeId(int ProductId)
        {
            List<ProductAttributeTable> getProductList = new List<ProductAttributeTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductAttributeTable>("SELECT * FROM TblProductAttributeView WHERE AttributeId=" + ProductId).ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductAttributeResponse.InsertData> InsertProductAttribute(ProductAttributeTable objProductAttributeTable)
        {
            List<ProductAttributeResponse.InsertData> getProductCategoryList = new List<ProductAttributeResponse.InsertData>();
            try
            {
                var values = new { AttributeId = objProductAttributeTable.AttributeId, ProductId = objProductAttributeTable.ProductId, AttributeValue = objProductAttributeTable.AttributeValue, Action = "1" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductAttributeResponse.InsertData>("[dbo].[InsertOrUpdateOrDeleteProductAttribute]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductAttributeResponse.DeleteData> DeleteProductAttribute(int AttributeId,long ProductId)
        {
            List<ProductAttributeResponse.DeleteData> objProductAttrLookup = new List<ProductAttributeResponse.DeleteData>();
            try
            {
                var values = new { AttributeId = AttributeId, ProductId = ProductId, AttributeValue = "", Action = "3" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    objProductAttrLookup = con.Query<ProductAttributeResponse.DeleteData>("[dbo].[InsertOrUpdateOrDeleteProductAttribute]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return objProductAttrLookup;
            }
            catch (Exception)
            {
                return objProductAttrLookup;
            }
        }
        public List<ProductAttributeResponse.UpdateData> UpdateProductAttribute(ProductAttributeTable objProductAttributeTable)
        {
            List<ProductAttributeResponse.UpdateData> getProductCategoryList = new List<ProductAttributeResponse.UpdateData>();
            try
            {
                var values = new { AttributeId = objProductAttributeTable.AttributeId, ProductId = objProductAttributeTable.ProductId, AttributeValue = objProductAttributeTable.AttributeValue, Action = "2" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductAttributeResponse.UpdateData>("[dbo].[InsertOrUpdateOrDeleteProductAttribute]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
    }
}
