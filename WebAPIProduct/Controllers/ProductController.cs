using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProduct.BusinessModel;
using WebAPIProduct.DapperORM;
using WebAPIProduct.DataAccess;
using static WebAPIProduct.BusinessModel.CommonResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        MyActionFilter objAutorized;
        public ProductController(MyActionFilter obj)
        {
            objAutorized = obj;
        }
        // GET: api/<ProductController>
        [ServiceFilter(typeof(MyActionFilter))]
        [HttpGet]
        public ActionResult Get()
        {
            if (objAutorized.ValidateTokenResult == null)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
            }
            if (objAutorized.ValidateTokenResult.isValid == false)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
            }

            ProductDBAccess obj = ProductDBAccess.getInstance;
            List<ProductTable> objListData = obj.GetALLProduct();
            return Ok(
                    new ProductResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product data found."
                    }
                    );

        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Get(int id)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                ProductDBAccess obj = ProductDBAccess.getInstance;
                List<ProductTable> objListData = obj.GetProductById(id);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Product not found." });
                }
                return Ok(
                    new ProductResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product data found."
                    }
                    );
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Post(ProductInsertParam objProductInsertParam)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                ProductTable objProduct = new ProductTable();
                objProduct.ProdCatId = objProductInsertParam.ProdCatId;
                objProduct.ProdName = objProductInsertParam.ProdName;
                objProduct.ProdDescription = objProductInsertParam.ProdDescription;
                ValidationContext vc = new ValidationContext(objProduct);
                var validationsResults = new List<ValidationResult>();
                bool correct = Validator.TryValidateObject(objProduct, vc, validationsResults, true);
                if (!correct)
                {
                    string errorMessage = "";
                    foreach (var item in validationsResults.Select(ov => ov.ErrorMessage))
                    {
                        errorMessage = string.Concat(errorMessage, item);
                    }
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = errorMessage });
                }
                ProductDBAccess obj = ProductDBAccess.getInstance;
                List<ProductResponse.InsertData> objListData = obj.InsertProduct(objProduct);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data insert." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductResponse.InsertData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    RowEffect = objListData[0].RowEffect,
                    ProductId = objListData[0].ProductId

                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }

        }

        // PUT api/<ProductController>
        [HttpPut]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Put(ProductUpdateParam objProductUpdateParam)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                ProductTable objProduct = new ProductTable();
                objProduct.ProductId = objProductUpdateParam.ProductId;
                objProduct.ProdCatId = objProductUpdateParam.ProdCatId;
                objProduct.ProdName = objProductUpdateParam.ProdName;
                objProduct.ProdDescription = objProductUpdateParam.ProdDescription;
                ValidationContext vc = new ValidationContext(objProduct);
                var validationsResults = new List<ValidationResult>();
                bool correct = Validator.TryValidateObject(objProduct, vc, validationsResults, true);
                if (!correct)
                {
                    string errorMessage = "";
                    foreach (var item in validationsResults.Select(ov => ov.ErrorMessage))
                    {
                        errorMessage = string.Concat(errorMessage, item);
                    }
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = errorMessage });
                }
                ProductDBAccess obj = ProductDBAccess.getInstance;
                List<ProductResponse.UpdateData> objListData = obj.UpdateProduct(objProduct);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data update." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductResponse.UpdateData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    RowEffect = objListData[0].RowEffect
                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{ProductId}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Delete(int ProductId)
        {
            try
            {
                if (objAutorized.ValidateTokenResult == null)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid access." });
                }
                if (objAutorized.ValidateTokenResult.isValid == false)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objAutorized.ValidateTokenResult.error_message });
                }
                if (ProductId < 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid product id." });
                }

                ProductDBAccess obj = ProductDBAccess.getInstance;
                List<ProductResponse.DeleteData> objListData = obj.DeleteProduct(ProductId);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data found." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductResponse.DeleteData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    RowEffect = objListData[0].RowEffect
                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }
    }
}
