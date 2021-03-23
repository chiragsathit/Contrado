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
    public sealed class ProductAttributeLookupDBAccess
    {

        private static readonly Lazy<ProductAttributeLookupDBAccess> instance = new Lazy<ProductAttributeLookupDBAccess>(() => new ProductAttributeLookupDBAccess());

        public static ProductAttributeLookupDBAccess getInstance
        {
            get
            {
                return instance.Value;
            }
        }
        public List<ProductAttributeLookupTable> GetALLProductAttributeLookup()
        {
            List<ProductAttributeLookupTable> getProductList = new List<ProductAttributeLookupTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductAttributeLookupTable>("SELECT * FROM TblProductAttributeLookupView").ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductAttributeLookupTable> GetProductAttributeLookupById(int AttributeId)
        {
            List<ProductAttributeLookupTable> getProductList = new List<ProductAttributeLookupTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductAttributeLookupTable>("SELECT * FROM TblProductAttributeLookupView WHERE AttributeId=" + AttributeId).ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductAttributeLookupTable> GetProductAttributeLookupByCategoryId(int ProdCatId)
        {
            List<ProductAttributeLookupTable> getProductList = new List<ProductAttributeLookupTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductAttributeLookupTable>("SELECT * FROM TblProductAttributeLookupView WHERE ProdCatId=" + ProdCatId).ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductAttributeLookupResponse.InsertData> InsertProductAttributeLookup(ProductAttributeLookupTable objProductAttributeLookupTable)
        {
            List<ProductAttributeLookupResponse.InsertData> getProductCategoryList = new List<ProductAttributeLookupResponse.InsertData>();
            try
            {
                var values = new { ProdCatId = objProductAttributeLookupTable.ProdCatId, AttributeName = objProductAttributeLookupTable.AttributeName, Action = "1" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductAttributeLookupResponse.InsertData>("[dbo].[InsertOrUpdateOrDeleteProductAttributeLookup]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductAttributeLookupResponse.DeleteData> DeleteProductAttributeLookup(int AttributeId)
        {
            List<ProductAttributeLookupResponse.DeleteData> objProductAttrLookup = new List<ProductAttributeLookupResponse.DeleteData>();
            try
            {
                var values = new { AttributeId = AttributeId,ProdCatId=0, AttributeName = "", Action = "3" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    objProductAttrLookup = con.Query<ProductAttributeLookupResponse.DeleteData>("[dbo].[InsertOrUpdateOrDeleteProductAttributeLookup]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return objProductAttrLookup;
            }
            catch (Exception)
            {
                return objProductAttrLookup;
            }
        }
        public List<ProductAttributeLookupResponse.UpdateData> UpdateProductAttributeLookup(ProductAttributeLookupTable objProductAttributeLookupTable)
        {
            List<ProductAttributeLookupResponse.UpdateData> getProductCategoryList = new List<ProductAttributeLookupResponse.UpdateData>();
            try
            {
                var values = new { AttributeId = objProductAttributeLookupTable.AttributeId, ProdCatId = objProductAttributeLookupTable.ProdCatId, AttributeName = objProductAttributeLookupTable.AttributeName, Action = "2" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductAttributeLookupResponse.UpdateData>("[dbo].[InsertOrUpdateOrDeleteProductAttributeLookup]", values, commandType: CommandType.StoredProcedure).ToList();
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
