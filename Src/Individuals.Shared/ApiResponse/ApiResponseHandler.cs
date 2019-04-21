using System.Net;

namespace Individuals.Shared.ApiResponse
{
    public class ApiResponseHandler
    {
        public static ApiResponse GenerateResponse(ResultType resultType, object result = default(object),
            ApiError error = null)
        {
            switch (resultType)
            {
                case ResultType.Ok:
                    return new ApiResponse(200,"მოთხოვნა წარმატებით დამუშავდა",result);
                case ResultType.NoContent:
                    return new ApiResponse(204, "მოთხოვნა წარმატებით დამუშავდა");
                case ResultType.Created:
                    return new ApiResponse(201, "რესურსის შექმნაზე მოთხოვნა წარმატებით დამუშავდა",result);
                case ResultType.BadRequest:
                    return new ApiResponse(403, "მოთხოვნა ვერ დამუშავდა, გადაამოწმეთ მიმართვის სისწორე", null, error);

                case ResultType.Forbidden:
                    return new ApiResponse(403, "მოთხოვნა ვერ დამუშავდა,თქვენ არ გაქვთ ამ რესურსზე წვდომის უფლება",null,error);
                case ResultType.NotFound:
                    return new ApiResponse(404, "მოთხოვნა ვერ დამუშავდა,თქვენ მიერ მოთხოვნილი რესურსის პოვნა ვერ მოხერხდა",null,error);
                case ResultType.InternalServerError:
                    return new ApiResponse(500, "მოთხოვნა ვერ დამუშავდა, დაფიქსირდა სერვერული შეცდომა",null,error);
                case ResultType.UnsupportedMediaType:
                    return new ApiResponse(415, "პარამეტრები არასწორია",null,error);
                default:
                    return new ApiResponse(400,"მოთხოვნა ვერ დამუშავდა, გადაამოწმეთ მიმართვის სისწორე");
            }
        }

        public static ApiResponse GenerateInternalError()
        {
            return new ApiResponse(500, "მოთხოვნა ვერ დამუშავდა, დაფიქსირდა სერვერული შეცდომა");
        }
    }
}
