using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Individuals.Shared.ApiResponse
{
    [DataContract]
    public class ApiResponse
    {
        [DataMember(Order = 1)]
        public int StatusCode { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 3)]
        public object Result { get; set; }

        [DataMember(EmitDefaultValue = false,Order = 4)]
        public ApiError Error { get; set; }

        public ApiResponse(int statusCode, string message = "", object result = default(object), ApiError error = null) 
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
            Result = result;
            Error = error;
        }
        public ApiResponse(int statusCode,string message = "",ApiError error = null)
        {
            StatusCode = statusCode;
            Message = message;
            Error = error;
        }

        public ApiResponse(int statusCode, string message = "")
        {
            StatusCode = statusCode;
            Message = message;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    
}
