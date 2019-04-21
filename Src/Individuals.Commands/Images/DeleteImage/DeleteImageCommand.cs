using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Images.DeleteImage
{
    public class DeleteImageCommand:IRequest<Result>
    {
        public long? Id { get; set; }
    }
}
