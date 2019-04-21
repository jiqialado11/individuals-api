using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Images.DeleteImage
{
    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class DeleteImageCommandHandler:IRequestHandler<DeleteImageCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;

        public DeleteImageCommandHandler(IIndividualsRepository individualsRepository)
        {
            _individualsRepository = individualsRepository;
        }

        public async Task<Result> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var individual = await _individualsRepository.FindSingle(request.Id.Value);
            if (individual == null)
                return Result.NotFound("Couldn't find related resource with identifier");

            var directory = new DirectoryInfo((Path.Combine(Environment.CurrentDirectory, "Images")));

            var files = directory.GetFiles("*" + $"{individual.FirstName}-{individual.LastName}-{individual.Id}" + "*.*");

            foreach (var file in files)
            {
                File.Delete(file.FullName);
            }

            individual.RemoveImage();

            _individualsRepository.Update(individual);

            return Result.OK(ResultType.NoContent);
        }
    }
}
