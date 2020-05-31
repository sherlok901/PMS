using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Business.Queries;

namespace PMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            var query = new GetProjectsInProgress { Date = DateTime.Now};
            var result = await _mediator.Send(query);
            if(result == null)
                return NotFound();

            Response.ContentType = new MediaTypeHeaderValue("application/octet-stream").ToString();// Content type

            result.FileData.Position = 0;
            byte[] bytes = result.FileData.ToArray();

            return File(bytes, "text/xlsx", "export.xlsx");
        }
    }
}