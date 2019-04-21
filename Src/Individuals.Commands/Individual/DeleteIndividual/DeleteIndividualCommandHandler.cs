using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.DeleteIndividual
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class DeleteIndividualCommandHandler:IRequestHandler<DeleteIndividualCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;

        public DeleteIndividualCommandHandler(IIndividualsRepository individualsRepository)
        {
            _individualsRepository = individualsRepository;
        }

        public async Task<Result> Handle(DeleteIndividualCommand request, CancellationToken cancellationToken)
        {
            var individual = await _individualsRepository.FindSingle(request.Id.Value);
            if(individual == null)
                return  Result.NotFound("Couldn't find resource with provided identifier");

            await _individualsRepository.Delete(request.Id.Value);
            return Result.OK(ResultType.NoContent);
        }
    }
}
