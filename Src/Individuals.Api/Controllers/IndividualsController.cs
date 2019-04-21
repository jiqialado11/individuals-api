using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Individuals.Api.Models;
using Individuals.Commands.Images.AddImage;
using Individuals.Commands.Images.DeleteImage;
using Individuals.Commands.Individual.CreateIndividual;
using Individuals.Commands.Individual.DeleteIndividual;
using Individuals.Commands.Individual.RemoveConnection;
using Individuals.Commands.Individual.SetConnectedIndividual;
using Individuals.Commands.Individual.UpdateIndividual;
using Individuals.Commands.PhoneNumber.AddPhoneNumber;
using Individuals.Commands.PhoneNumber.DeletePhoneNumber;
using Individuals.Commands.PhoneNumber.UpdatePhoneNumber;
using Individuals.Queries.Individuals.GetIndividual;
using Individuals.Queries.Individuals.QueryIndividuals;
using Individuals.Shared.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Individuals.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualsController:ControllerBase
    {
        private readonly IMediator _mediator;

        public IndividualsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse> QueryIndividuals([FromQuery] QueryIndividualsModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<QueryIndividualsModel, QueryIndividualsQuery>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpGet("{Id}")]
        public async Task<ApiResponse> GetIndividualById([FromRoute] GetIndividualModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<GetIndividualModel, GetIndividualQuery>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpPost]
        public async Task<ApiResponse> CreateIndividual([FromBody] CreateIndividualModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<CreateIndividualModel, CreateIndividualCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpPut]
        public async Task<ApiResponse> UpdateIndividual([FromBody] UpdateIndividualModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<UpdateIndividualModel, UpdateIndividualCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpDelete("{Id}")]
        public async Task<ApiResponse> DeleteIndividual([FromRoute] DeleteIndividualModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<DeleteIndividualModel, DeleteIndividualCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpPost("PhoneNumber")]
        public async Task<ApiResponse> AddPhoneNumbers([FromBody] AddPhoneNumbersModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<AddPhoneNumbersModel, AddPhoneNumberCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpPut("PhoneNumber")]
        public async Task<ApiResponse> UpdatePhoneNumber([FromBody] UpdatePhoneNumberModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<UpdatePhoneNumberModel, UpdatePhoneNumberCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpDelete("PhoneNumber/{Id}")]
        public async Task<ApiResponse> DeletePhoneNumber([FromRoute] DeletePhoneNumberModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<DeletePhoneNumberModel, DeletePhoneNumberCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpPost("Images/{Id}")]
        public async Task<ApiResponse> UploadImage( IFormFile file, [FromRoute] UploadImageModel model)
        {
            var stream = new MemoryStream();
            file.CopyTo(stream);

            var command = new AddImageCommand {Id = model.Id.Value, FileName = file.FileName, FileStream = stream};
           var response=  await _mediator.Send(command);

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpDelete("Images/{Id}")]
        public async Task<ApiResponse> DeleteImage([FromRoute] DeleteImageModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<DeleteImageModel, DeleteImageCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpPost("ConnectedIndividual")]
        public async Task<ApiResponse> SetConnectedIndividual([FromBody] SetConnectedIndividualModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<SetConnectedIndividualModel, SetConnectedIndividualCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }

        [HttpDelete("ConnectedIndividual")]
        public async Task<ApiResponse> RemoveConnectionFromIndividuals([FromBody] RemoveConnectionModel model)
        {
            var response =
                await _mediator.Send(
                    Mapper.Map<RemoveConnectionModel, RemoveConnectionCommand>(model));

            if (response.IsSuccess)
                return ApiResponseHandler.GenerateResponse(response.Type, response.Data);

            return ApiResponseHandler.GenerateResponse(response.Type, null, ApiError.GenerateError(response));
        }
    }
}
