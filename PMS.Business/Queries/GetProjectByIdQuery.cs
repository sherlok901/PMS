using MediatR;
using PMS.Business.Responses;

namespace PMS.Business.Queries
{
    public class GetProjectByIdQuery : IRequest<ProjectResponse>
    {
        public int Id { get; set; }

        public GetProjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}
