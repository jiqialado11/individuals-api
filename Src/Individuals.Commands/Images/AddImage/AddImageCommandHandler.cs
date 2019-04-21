using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Individuals.Decorators;
using Individuals.Decorators.Decorators;
using Individuals.Persistance.Repositories.Contracts;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Images.AddImage
{

    [BaseDecorator(typeof(ValidationDecorator<,>))]
    [BaseDecorator(typeof(TransactionDecorator<,>))]
    public class AddImageCommandHandler:IRequestHandler<AddImageCommand,Result>
    {
        private readonly IIndividualsRepository _individualsRepository;

        public AddImageCommandHandler(IIndividualsRepository individualsRepository)
        {
            _individualsRepository = individualsRepository;
        }

        public async Task<Result> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {

            var individual = await _individualsRepository.FindSingle(request.Id.Value);
            if(individual == null)
                return Result.NotFound("Couldn't find related resource with identifier");

            var oldFileName =Path.GetFileNameWithoutExtension(request.FileName) ;
            var newFileName = $"{individual.FirstName}-{individual.LastName}-{individual.Id}";

            var fileName = request.FileName.Replace(oldFileName, newFileName);

            var directory  = Path.Combine(Environment.CurrentDirectory,"Images") ;
            var filePath = Path.Combine(directory, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
            
          
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.FileStream.CopyToAsync(stream, cancellationToken);
            }

            
            individual.SetImage(filePath); 
            _individualsRepository.Update(individual);

            return Result.OK(ResultType.Created);
         
        }
    }
}
