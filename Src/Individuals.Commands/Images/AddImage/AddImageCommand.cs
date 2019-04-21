using System.IO;
using Individuals.Shared;
using MediatR;

namespace Individuals.Commands.Images.AddImage
{
    public class AddImageCommand:IRequest<Result>
    {
        public long? Id { get; set; }
        public string FileName { get; set; }

        public Stream FileStream { get; set; }
    }
}
