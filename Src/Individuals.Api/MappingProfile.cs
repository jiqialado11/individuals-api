using AutoMapper;
using Individuals.Api.Models;
using Individuals.Commands.Images.DeleteImage;
using Individuals.Commands.Individual.CreateIndividual;
using Individuals.Commands.Individual.DeleteIndividual;
using Individuals.Commands.Individual.RemoveConnection;
using Individuals.Commands.Individual.SetConnectedIndividual;
using Individuals.Commands.Individual.UpdateIndividual;
using Individuals.Commands.PhoneNumber.AddPhoneNumber;
using Individuals.Commands.PhoneNumber.DeletePhoneNumber;
using Individuals.Commands.PhoneNumber.UpdatePhoneNumber;
using Individuals.DataAccess.Models;
using Individuals.DataAccess.Models.GetIndividual;
using Individuals.DataAccess.Models.QueryIndividuals;
using Individuals.Queries.Individuals.GetIndividual;
using Individuals.Queries.Individuals.QueryIndividuals;

namespace Individuals.Api
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<QueryIndividualsModel, QueryIndividualsQuery>();
            CreateMap<QueryIndividualsQuery, QueryIndividualsRequest>();
            CreateMap<GetIndividualModel, GetIndividualQuery>();
            CreateMap<GetIndividualQuery, GetIndividualRequest>();
            CreateMap<CreateIndividualModel, CreateIndividualCommand>();
            CreateMap<AddPhoneNumbersModel, AddPhoneNumberCommand>();
            CreateMap<UpdatePhoneNumberModel, UpdatePhoneNumberCommand>();
            CreateMap<DeletePhoneNumberModel, DeletePhoneNumberCommand>();
            CreateMap<UpdateIndividualModel, UpdateIndividualCommand>();
            CreateMap<DeleteIndividualModel, DeleteIndividualCommand>();
            CreateMap<DeleteImageModel, DeleteImageCommand>();
            CreateMap<SetConnectedIndividualModel, SetConnectedIndividualCommand>();
            CreateMap<RemoveConnectionModel, RemoveConnectionCommand>();


        }
    }
}
