using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Individuals.DataAccess.Contracts;
using Individuals.DataAccess.Models;
using Individuals.DataAccess.Models.GetIndividual;
using Individuals.DataAccess.Models.GetIndividual.Response_Objects;
using Individuals.DataAccess.Models.QueryIndividuals;
using Individuals.Shared.Configurations;
using Microsoft.Extensions.Options;

namespace Individuals.DataAccess
{
    public class IndividualsRelatedQueries:IIndividualsRelatedQueries
    {
        private readonly string _connectionString;

        public IndividualsRelatedQueries(IOptions<ConnectionStrings> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }
        public async Task<QueryIndividualsResponse> QueryIndividuals(QueryIndividualsRequest request)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                InitializeQueryIndividualsParameters(request, parameters);

                var response = await connection.QueryAsync<QueryIndividualsItem>("[dbo].[spQueryIndividuals]",
                    parameters, commandType: CommandType.StoredProcedure) as List<QueryIndividualsItem>;
                return new QueryIndividualsResponse
                { List = response.Any() ? response : new List<QueryIndividualsItem>() };
            }
        }
        private static void InitializeQueryIndividualsParameters(QueryIndividualsRequest request, DynamicParameters parameters)
        {
            if (!string.IsNullOrEmpty(request.QueryString))
                parameters.Add("@QueryString", request.QueryString);
            else
                parameters.Add("@QueryString", (string)null);
            if (!string.IsNullOrEmpty(request.Name))
                parameters.Add("@Name", request.Name);
            else
                parameters.Add("@Name", (string)null);
            if (!string.IsNullOrEmpty(request.LastName))
                parameters.Add("@LastName", request.LastName);
            else
                parameters.Add("@LastName", (string)null);
            if (!string.IsNullOrEmpty(request.PersonalId))
                parameters.Add("@PersonalId", request.PersonalId);
            else
                parameters.Add("@PersonalId", (string)null);
            if (!string.IsNullOrEmpty(request.City))
                parameters.Add("@City", request.City);
            else
                parameters.Add("@City", (string)null);
            if (request.Gender.HasValue)
                parameters.Add("@Gender", request.Gender.Value);
            else
                parameters.Add("@Gender", (int?)null);
            if (request.BirthDate.HasValue)
                parameters.Add("@BirthDate", request.BirthDate.Value);
            else
                parameters.Add("@BirthDate", (DateTime?)null);
            if (!string.IsNullOrEmpty(request.OrderBy))
                parameters.Add("@OrderBy", request.OrderBy);
            else
                parameters.Add("@OrderBy", (string)null);
            if (request.PageNumber.HasValue)
                parameters.Add("@PageNumber", request.PageNumber.Value);
            else
                parameters.Add("@PageNumber", (int?)null);
            if (request.PageSize.HasValue)
                parameters.Add("@PageSize", request.PageSize.Value);
            else
                parameters.Add("@PageSize", (int?)null);
        }

        public async Task<GetIndividualResponse> GetIndividual(GetIndividualRequest request)
        {
            var response = new GetIndividualResponse { Individual = null };
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = "dbo.spGetIndividual";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("@Id", request.Id.Value));
                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();
                    if (!reader.HasRows)
                        return response;

                     response = new GetIndividualResponse {Individual = new GetIndividualItem()};
                    await reader.ReadAsync();

                        response.Individual.Id =
                            reader["Id"] != null ? Convert.ToInt32(reader["Id"].ToString()) : default(int);
                        response.Individual.FirstName =
                            reader["FirstName"] != null ? reader["FirstName"].ToString() : string.Empty;
                        response.Individual.LastName =
                            reader["LastName"] != null ? reader["LastName"].ToString() : string.Empty;
                        response.Individual.PersonalId =
                            reader["PersonalId"] != null ? reader["PersonalId"].ToString(): string.Empty;
                        response.Individual.Gender = reader["Gender"] != null
                            ? Convert.ToInt32(reader["Gender"].ToString())
                            : (int?)null;
                        response.Individual.BirthDate = reader["BirthDate"] != null
                            ? Convert.ToDateTime(reader["BirthDate"].ToString())
                            : (DateTime?)null;
                        response.Individual.ImagePath = reader["ImagePath"] != null
                            ? reader["ImagePath"].ToString()
                            : string.Empty;
                        response.Individual.CityId = reader["CityId"] != null
                            ? Convert.ToInt32(reader["CityId"].ToString())
                            : (int?)null;
                        response.Individual.City = reader["City"] != null ? reader["City"].ToString() : string.Empty;


                     await reader.NextResultAsync();
                    if (reader.HasRows)
                    {
                        response.Individual.ConnectedIndividuals = new List<ConnectedIndividualItem>();
                        while (await reader.ReadAsync())
                        {
                            var individual = new ConnectedIndividualItem
                            {
                                Id = reader["Id"] != null ? Convert.ToInt32(reader["Id"]) : default(int),
                                FirstName =
                                    reader["FirstName"] != null ? reader["FirstName"].ToString() : string.Empty,
                                LastName =
                                    reader["LastName"] != null ? reader["LastName"].ToString() : string.Empty,
                                PersonalId =
                                    reader["PersonalId"] != null ? reader["PersonalId"].ToString() : string.Empty,
                                Gender = reader["Gender"] != null
                                    ? Convert.ToInt32(reader["Gender"].ToString())
                                    : (int?) null,
                                BirthDate = reader["BirthDate"] != null
                                    ? Convert.ToDateTime(reader["BirthDate"].ToString())
                                    : (DateTime?) null,
                                ImagePath = reader["ImagePath"] != null
                                    ? reader["ImagePath"].ToString()
                                    : string.Empty,
                                ConnectionType = reader["ConnectionType"] != null
                                    ? Convert.ToInt32(reader["ConnectionType"].ToString())
                                    : (int?) null
                            };
                            response.Individual.ConnectedIndividuals.Add(individual);
                        }
                    }

                    await reader.NextResultAsync();
                    if (reader.HasRows)
                    {
                        response.Individual.PhoneNumbers=new List<PhoneNumberItem>();

                        while (await reader.ReadAsync())
                        {
                            var phone = new PhoneNumberItem
                            {
                                PhoneNumberId = reader["PhoneNumberId"] != null
                                    ? Convert.ToInt32(reader["PhoneNumberId"].ToString())
                                    : default(int),
                                PhoneNumber = reader["PhoneNumber"] != null
                                    ? reader["PhoneNumber"].ToString()
                                    : string.Empty,
                                PhoneNumberType = reader["PhoneNumberType"] != null
                                    ? Convert.ToInt32(reader["PhoneNumberType"].ToString())
                                    : (int?) null
                            };
                            response.Individual.PhoneNumbers.Add(phone);
                        }
                    }

                }
            }

            return response;
        }

    
    }
}
