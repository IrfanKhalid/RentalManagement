using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using WebGYM.Common;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IPaymentDetails _paymentDetails;
        private readonly IUrlHelper _urlHelper;
        public ExpenseController(IUrlHelper urlHelper, IPaymentDetails paymentDetails)
        {
            _paymentDetails = paymentDetails;
            _urlHelper = urlHelper;
        }

        // GET: api/Payment
        [HttpGet(Name = "GetAllPaymentExpense")]
        public IActionResult GetAllExpense([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var userId = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.Name));

                List<ExpenseViewModel> allMembers = _paymentDetails.GetAllExpense(queryParameters,userId); 

                var allItemCount = _paymentDetails.CountExpense(queryParameters.ToString());

                var paginationMetadata = new
                {
                    totalCount = allItemCount,
                    pageSize = queryParameters.PageCount,
                    currentPage = queryParameters.Page,
                    totalPages = queryParameters.GetTotalPages(allItemCount)
                };

                Request.HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

                return Ok(new
                {
                    value = allMembers
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [SwaggerResponse(200, "The product was created", typeof(ExpenseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("SaveExpense")]
        public HttpResponseMessage SaveExpensePayment( [FromBody, SwaggerRequestBody("The product payload", Required = true)] ExpenseViewModel member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (!_memberRegistration.CheckNameExits())
                    //{
                    var userId = this.User.FindFirstValue(ClaimTypes.Name);
                    var automember = AutoMapper.Mapper.Map<Expense>(member);
                    var result = _paymentDetails.InsertExpense(automember);
                    if (result > 0)
                    {
                        var response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.OK
                        };
                        return response;
                    }
                    else
                    {
                        var response = new HttpResponseMessage()
                        {
                            StatusCode = HttpStatusCode.BadRequest
                        };
                        return response;
                    }
                    //}
                    //else
                    //{
                    //    var response = new HttpResponseMessage()
                    //    {
                    //        StatusCode = HttpStatusCode.Conflict
                    //    };
                    //    return response;
                    //}
                }
                else
                {
                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    return response;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}