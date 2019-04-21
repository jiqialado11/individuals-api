using System;
using System.Collections.Generic;
using System.Linq;

namespace Individuals.Shared.ApiResponse
{
    public class ApiError
    {
        public string ExceptionMessage { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }

        public ApiError()
        {
            
        }

        public ApiError(string message)
        {
            ExceptionMessage = message;
        }

        public static ApiError GenerateError(Result result)
        {
            var error = new ApiError();
            if (result.Exception != null)
                error.ExceptionMessage = result.Exception.Message;

            if (!string.IsNullOrEmpty(result.Message))
                error.ExceptionMessage = result.Message;

            if (result.Errors != null && result.Errors.Any())
                error.ValidationErrors = result.Errors.Select(x => new ValidationError(x.Key, x.Value)).ToList();
           
            return error;
        }
    }
}
