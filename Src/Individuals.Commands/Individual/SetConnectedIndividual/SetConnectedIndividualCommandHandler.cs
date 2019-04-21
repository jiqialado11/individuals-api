using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Domain.Entities;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Individual.SetConnectedIndividual
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class SetConnectedIndividualCommandHandler:IRequestHandler<SetConnectedIndividualCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;

        public SetConnectedIndividualCommandHandler(IIndividualsRepository individualsRepository)
        {
            _individualsRepository = individualsRepository;
        }

        public async Task<Result> Handle(SetConnectedIndividualCommand request, CancellationToken cancellationToken)
        {
            var connectedFromIndividual = await _individualsRepository.FindSingle(request.ConnectedFromIndividualId.Value);
            if (connectedFromIndividual == null)
                return Result.NotFound("Couldn't find related resource with identifier ");

            var connectedToIndividual = await _individualsRepository.FindSingle(request.ConnectedToIndividualId.Value);
            if (connectedToIndividual == null)
                return Result.NotFound("Couldn't find related resource with identifier ");


            var verifyConnection = await _individualsRepository.VerifyConnection(request.ConnectedFromIndividualId.Value,
                request.ConnectedToIndividualId.Value);

            if(verifyConnection!= null)
                return Result.Error(ResultType.BadRequest,"Connection already exists");

            var individualConnection = new ConnectedIndividual();
            individualConnection.SetIndividualsConnection(connectedFromIndividual,connectedToIndividual,request.ConnectionType.Value);

            connectedFromIndividual.SetConnection(individualConnection);

            _individualsRepository.Update(connectedFromIndividual);

            return Result.OK(ResultType.Created);

        }
    }
}
