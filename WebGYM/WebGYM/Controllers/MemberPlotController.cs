using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebGYM.Common;
using WebGYM.Interface;
using WebGYM.Models;
using WebGYM.ViewModels;

namespace WebGYM.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class MemberPlotController:ControllerBase
    {

        private readonly IMemberRegistration _memberRegistration;
        private readonly IUrlHelper _urlHelper;

        public MemberPlotController(IUrlHelper urlHelper, IMemberRegistration memberRegistration)
        {
            _memberRegistration = memberRegistration;
            _urlHelper = urlHelper;
        }
        // GET: api/GetAllPlot
        [HttpGet("GetAllPlot/{id}")]
        //[Route("GetAllPlot/{id}")]
        public IActionResult GetAllPlot([FromQuery] QueryParameters queryParameters, int id)
        {
           var userId = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.Name));
            List<MemberPlotGridModel> allMembers = _memberRegistration.GetAllPlot(queryParameters, id).ToList();

            var allItemCount = _memberRegistration.PlotCount(id);

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

    }
}
