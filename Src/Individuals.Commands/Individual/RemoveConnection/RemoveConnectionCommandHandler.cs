using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.RemoveConnection
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class RemoveConnectionCommandHandler:IRequestHandler<RemoveConnectionCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;

        public RemoveConnectionCommandHandler(IIndividualsRepository individualsRepository)
        {
            _individualsRepository = individualsRepository;
        }

        public async Task<Result> Handle(RemoveConnectionCommand request, CancellationToken cancellationToken)
        {
            var connectedFromIndividual = await _individualsRepository.FindSingle(request.ConnectedFromIndividualId.Value);
            if (connectedFromIndividual == null)
                return Result.NotFound("Couldn't find related resource with identifier ");

            var connectedToIndividual = await _individualsRepository.FindSingle(request.ConnectedToIndividualId.Value);
            if (connectedToIndividual == null)
                return Result.NotFound("Couldn't find related resource with identifier ");


              await _individualsRepository.RemoveConnection(request.ConnectedFromIndividualId.Value,
                request.ConnectedToIndividualId.Value);

            return Result.OK(ResultType.NoContent);


        }
    }
}
