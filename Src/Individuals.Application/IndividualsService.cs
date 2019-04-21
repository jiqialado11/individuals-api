using System.Threading.Tasks;
using AutoMapper;
using Individuals.Application.Contracts;
using Individuals.Application.Models;
using Individuals.DataAccess.Models.Primitive_Models;
using Individuals.Queries.Individuals.QueryIndividuals;
using MediatR;

namespace Individuals.Application
{
    public class IndividualsService:IIndividualsService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public IndividualsService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<QueryIndividualsResponse> QueryIndividuals(QueryIndividualsRequest request)
        {
            var queryObject = _mapper.Map<QueryIndividualsRequest, QueryIndividualsQuery>(request);
            var queryResponse = await _mediator.Send(queryObject);

            if(queryResponse.IsSuccess)
            {
                
            }
        }
    }
}
