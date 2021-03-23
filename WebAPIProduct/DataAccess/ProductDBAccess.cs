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
    public sealed class ProductDBAccess
    {

        private static readonly Lazy<ProductDBAccess> instance = new Lazy<ProductDBAccess>(() => new ProductDBAccess());

        public static ProductDBAccess getInstance
        {
            get
            {
                return instance.Value;
            }
        }
        public List<ProductTable> GetALLProduct()
        {
            List<ProductTable> getProductList = new List<ProductTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductTable>("SELECT * FROM TblProductView").ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductTable> GetProductById(int ProductId)
        {
            List<ProductTable> getProductList = new List<ProductTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductTable>("SELECT * FROM TblProductView WHERE ProductId=" + ProductId).ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductTable> GetProductByCategoryId(int ProdCatId)
        {
            List<ProductTable> getProductList = new List<ProductTable>();
            try
            {
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductList = con.Query<ProductTable>("SELECT * FROM TblProductView WHERE ProdCatId=" + ProdCatId).ToList();
                }
                return getProductList;
            }
            catch (Exception)
            {
                return getProductList;
            }
        }
        public List<ProductResponse.InsertData> InsertProduct(ProductTable objProductTable)
        {
            List<ProductResponse.InsertData> getProductCategoryList = new List<ProductResponse.InsertData>();
            try
            {
                var values = new { ProductId = objProductTable.ProductId, ProdCatId = objProductTable.ProdCatId, ProdName = objProductTable.ProdName, ProdDescription = objProductTable.ProdDescription, Action = "1" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductResponse.InsertData>("[dbo].[InsertOrUpdateOrDeleteProduct]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductResponse.DeleteData> DeleteProduct(int ProductId)
        {
            List<ProductResponse.DeleteData> getProductCategoryList = new List<ProductResponse.DeleteData>();
            try
            {
                var values = new { ProductId = ProductId, ProdCatId = 0, ProdName = "", ProdDescription = "", Action = "3" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductResponse.DeleteData>("[dbo].[InsertOrUpdateOrDeleteProduct]", values, commandType: CommandType.StoredProcedure).ToList();
                }
                return getProductCategoryList;
            }
            catch (Exception)
            {
                return getProductCategoryList;
            }
        }
        public List<ProductResponse.UpdateData> UpdateProduct(ProductTable objProductTable)
        {
            List<ProductResponse.UpdateData> getProductCategoryList = new List<ProductResponse.UpdateData>();
            try
            {
                var values = new { ProductId = objProductTable.ProductId, ProdCatId = objProductTable.ProdCatId, ProdName = objProductTable.ProdName, ProdDescription = objProductTable.ProdDescription, Action = "2" };
                using (IDbConnection con = new SqlConnection(ConString.GetConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    getProductCategoryList = con.Query<ProductResponse.UpdateData>("[dbo].[InsertOrUpdateOrDeleteProduct]", values, commandType: CommandType.StoredProcedure).ToList();
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
