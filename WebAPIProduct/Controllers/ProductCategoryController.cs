using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text.Json;
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
    public class ProductCategoryController : ControllerBase
    {
        MyActionFilter objAutorized;
        public ProductCategoryController(MyActionFilter obj)
        {
            objAutorized = obj;
        }
        // GET: api/<ProductCategoryController>
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

            ProductCategoryDBAccess obj = ProductCategoryDBAccess.getInstance;
            List<ProductCategoryTable> objListData = obj.GetProductALLCategory();
            return Ok(
                    new ProductCategoryResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product category data found."
                    }
                    );

        }

        // GET api/<ProductCategoryController>/5
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
                ProductCategoryDBAccess obj = ProductCategoryDBAccess.getInstance;
                List<ProductCategoryTable> objListData = obj.GetProductCategoryById(id);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Product category not found." });
                }
                return Ok(
                    new ProductCategoryResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product category data found."
                    }
                    );
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
        }

        // POST api/<ProductCategoryController>
        [HttpPost]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Post(ProductCategoryInsertParam objProductCategoryInsertParam)
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
                ProductCategoryTable objProductCategory = new ProductCategoryTable();
                objProductCategory.CategoryName = objProductCategoryInsertParam.CategoryName;
                ValidationContext vc = new ValidationContext(objProductCategory);
                var validationsResults = new List<ValidationResult>();
                bool correct = Validator.TryValidateObject(objProductCategory, vc, validationsResults, true);
                if (!correct)
                {
                    string errorMessage = "";
                    foreach (var item in validationsResults.Select(ov => ov.ErrorMessage))
                    {
                        errorMessage = string.Concat(errorMessage, item);
                    }
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = errorMessage });
                }
                ProductCategoryDBAccess obj = ProductCategoryDBAccess.getInstance;
                List<ProductCategoryResponse.InsertData> objListData = obj.InsertProductCategory(objProductCategory);
                if(objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data insert." });
                }

                if(objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductCategoryResponse.InsertData()
                {
                    error_code = Convert.ToInt32(ErrorCode.Success),
                    message = objListData[0].message,
                    ProdCatId = objListData[0].ProdCatId,
                     RowEffect= objListData[0].RowEffect

                });

            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = ex.Message });
            }
           
        }

        // PUT api/<ProductCategoryController>
        [HttpPut]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Put(ProductCategoryUpdateParam objProductCategoryUpdateParam)
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
                ProductCategoryTable objProductCategory = new ProductCategoryTable();
                objProductCategory.ProdCatId = objProductCategoryUpdateParam.ProdCatId;
                objProductCategory.CategoryName = objProductCategoryUpdateParam.CategoryName;
                ValidationContext vc = new ValidationContext(objProductCategory);
                var validationsResults = new List<ValidationResult>();
                bool correct = Validator.TryValidateObject(objProductCategory, vc, validationsResults, true);
                if (!correct)
                {
                    string errorMessage = "";
                    foreach (var item in validationsResults.Select(ov => ov.ErrorMessage))
                    {
                        errorMessage = string.Concat(errorMessage, item);
                    }
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = errorMessage });
                }
                ProductCategoryDBAccess obj = ProductCategoryDBAccess.getInstance;
                List<ProductCategoryResponse.UpdateData> objListData = obj.UpdateProductCategory(objProductCategory);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data update." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductCategoryResponse.UpdateData()
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

        // DELETE api/<ProductCategoryController>/5
        [HttpDelete("{ProdCatId}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Delete(int ProdCatId)
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
                if (ProdCatId < 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid product category id." });
                }
                
                ProductCategoryDBAccess obj = ProductCategoryDBAccess.getInstance;
                List<ProductCategoryResponse.DeleteData> objListData = obj.DeleteProductCategory(ProdCatId);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data found." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductCategoryResponse.DeleteData()
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
