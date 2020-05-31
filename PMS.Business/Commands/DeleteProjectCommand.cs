using MediatR;
using PMS.Business.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Business.Commands
{
    public class DeleteProjectCommand : IRequest<DeleteResponse>
    {
        public int Id { get; set; }
    }
}
