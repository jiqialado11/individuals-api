using System;
using System.Collections.Generic;
using MediatR;

namespace Individuals.Shared
{
    
    public class Result 
    {
        public object Data { get; private set; }

        protected readonly Dictionary<string, string> _errors = new Dictionary<string, string>();

        public bool IsSuccess => (Type == ResultType.Ok || Type== ResultType.Created || Type== ResultType.NoContent);
        public string Message { get; protected set; }
        public Exception Exception { get; protected set; }
        public IReadOnlyDictionary<string, string> Errors => _errors;
        public ResultType Type { get; }

        internal Result(ResultType type)
        {
            Type = type;
        }

        internal Result SetData(object data)
        {
            Data = data;
            return this;
        }

        internal Result SetMessage(string message)
        {
            Message = message;
            return this;
        }

        internal   Result SetInternalError(Exception exception)
        {
            Exception = exception;
            return this;
        }

        internal Result SetErrorMessages(IReadOnlyDictionary<string, string> errors)
        {
            if (errors != null)
                foreach (var keyValuePair in errors)
                {
                    _errors.Add(keyValuePair.Key, keyValuePair.Value);
                }
            return this;
        }

        public static Result Ok(object data) => new Result(ResultType.Ok).SetData(data);
        public static Result OK(ResultType type)=>new Result(type);
        public static Result OK(ResultType type,object data) => new Result(type).SetData(data);
        public static Result NotFound(string message) => new Result(ResultType.NotFound).SetMessage(message);

        public static Result Error(ResultType resultType, string message, Exception exception = null, IReadOnlyDictionary<string, string> errors = null) =>
            new Result(resultType).SetMessage(message).SetInternalError(exception).SetErrorMessages(errors);

    }

    public enum ResultType
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        UnsupportedMediaType = 415,
        InternalServerError=500
    }
}
