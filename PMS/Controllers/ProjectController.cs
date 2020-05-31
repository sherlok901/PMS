using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMS.Business.Commands;
using PMS.Business.Queries;
using PMS.Business.Responses;

namespace PMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id = 1)
        {
            var query = new GetProjectByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectCommand command)
        {
            //TODO: validate command
            var result = await _mediator.Send(command);
            return result != null ? (IActionResult)Ok(result) : BadRequest(new ValidationResponse { Code = 1, Message = "Validation message"});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO: validate command
            var result = await _mediator.Send(new DeleteProjectCommand { Id = id });
            return result != null ? (IActionResult)Ok(result) : BadRequest(new ValidationResponse { Code = 1, Message = "Validation message" });
        }
    }
}
