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
    public class ProductAttributeController : ControllerBase
    {
        MyActionFilter objAutorized;
        public ProductAttributeController(MyActionFilter obj)
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

            ProductAttributeDBAccess obj = ProductAttributeDBAccess.getInstance;
            List<ProductAttributeTable> objListData = obj.GetALLProductAttribute();
            return Ok(
                    new ProductAttributeResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product attribute value data found."
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
                ProductAttributeDBAccess obj = ProductAttributeDBAccess.getInstance;
                List<ProductAttributeTable> objListData = obj.GetProductAttributeByProductId(id);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Attribute value not found for product id." });
                }
                return Ok(
                    new ProductAttributeResponse.SelectData()
                    {
                        data = objListData,
                        error_code = Convert.ToInt32(ErrorCode.Success),
                        message = "Product attribute value data found."
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
        public ActionResult Post(ProductAttributeInsertParam objProductInsertParam)
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
                ProductAttributeTable objProduct = new ProductAttributeTable();
                objProduct.AttributeId = objProductInsertParam.AttributeId;
                objProduct.ProductId = objProductInsertParam.ProductId;
                objProduct.AttributeValue = objProductInsertParam.AttributeValue;
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
                ProductAttributeDBAccess obj = ProductAttributeDBAccess.getInstance;
                List<ProductAttributeResponse.InsertData> objListData = obj.InsertProductAttribute(objProduct);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data insert." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductAttributeResponse.InsertData()
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

        // PUT api/<ProductController>
        [HttpPut]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Put(ProductAttributeUpdateParam objProductUpdateParam)
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
                ProductAttributeTable objProduct = new ProductAttributeTable();
                objProduct.AttributeId = objProductUpdateParam.AttributeId;
                objProduct.ProductId = objProductUpdateParam.ProductId;
                objProduct.AttributeValue = objProductUpdateParam.AttributeValue;
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
                ProductAttributeDBAccess obj = ProductAttributeDBAccess.getInstance;
                List<ProductAttributeResponse.UpdateData> objListData = obj.UpdateProductAttribute(objProduct);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data update." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductAttributeResponse.UpdateData()
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
        [HttpDelete("{AttributeId}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult Delete(ProductAttributeDeleteParam objProductAttributeDeleteParam)
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
                if (objProductAttributeDeleteParam.AttributeId < 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid attribute id." });
                }
                if (objProductAttributeDeleteParam.ProductId < 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "Invalid attribute id." });
                }

                ProductAttributeDBAccess obj = ProductAttributeDBAccess.getInstance;
                List<ProductAttributeResponse.DeleteData> objListData = obj.DeleteProductAttribute(objProductAttributeDeleteParam.AttributeId, objProductAttributeDeleteParam.ProductId);
                if (objListData.Count == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = "No data found." });
                }

                if (objListData[0].RowEffect == 0)
                {
                    return Ok(new CommonResponse() { error_code = Convert.ToInt32(ErrorCode.Failure), message = objListData[0].message });
                }

                return Ok(new ProductAttributeResponse.DeleteData()
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
