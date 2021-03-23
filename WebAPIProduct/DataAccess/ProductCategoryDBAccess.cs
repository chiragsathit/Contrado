using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using WebAPIProduct.BusinessModel;
using WebAPIProduct.DapperORM;
namespace WebAPIProduct.DataAccess
{
    public sealed class ProductCategoryDBAccess
    {

        private static readonly Lazy<ProductCategoryDBAccess> instance = new Lazy<ProductCategoryDBAccess>(() => new ProductCategoryDBAccess());

        public static ProductCategoryDBAccess getInstance
        {
            get
            {
               return instance.Value;
            }
        }
        public List<ProductCategoryTable> GetProductALLCategory()
        {
            List<ProductCategoryTable> getProductCategoryList = new List<ProductCategoryTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductCategoryTable>("SELECT * FROM TblProductCategoryView").ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductCategoryTable> GetProductCategoryById(int ProdCatId)
        {
            List<ProductCategoryTable> getProductCategoryList = new List<ProductCategoryTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductCategoryTable>("SELECT * FROM TblProductCategoryView WHERE ProdCatId="+ ProdCatId).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductCategoryResponse.InsertData> InsertProductCategory(ProductCategoryTable objProductCategoryTable)
        {
            List<ProductCategoryResponse.InsertData> getProductCategoryList = new List<ProductCategoryResponse.InsertData>();
            try
            {
                var values = new { CategoryName= objProductCategoryTable.CategoryName, Action= "1" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductCategoryResponse.InsertData>("[dbo].[InsertOrUpdateOrDeleteProductCategory]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductCategoryResponse.DeleteData> DeleteProductCategory(int ProdCatId)
        {
            List<ProductCategoryResponse.DeleteData> getProductCategoryList = new List<ProductCategoryResponse.DeleteData>();
            try
            {
                var values = new { ProdCatId = ProdCatId,CategoryName = "", Action = "3" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductCategoryResponse.DeleteData>("[dbo].[InsertOrUpdateOrDeleteProductCategory]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductCategoryResponse.UpdateData> UpdateProductCategory(ProductCategoryTable objProductCategoryTable)
        {
            List<ProductCategoryResponse.UpdateData> getProductCategoryList = new List<ProductCategoryResponse.UpdateData>();
            try
            {
                var values = new { ProdCatId = objProductCategoryTable.ProdCatId, CategoryName = objProductCategoryTable.CategoryName, Action = "2" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductCategoryResponse.UpdateData>("[dbo].[InsertOrUpdateOrDeleteProductCategory]", values, commandType: CommandType.StoredProcedure).ToList();
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
