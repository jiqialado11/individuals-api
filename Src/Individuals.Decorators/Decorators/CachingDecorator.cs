using System;
using System.Threading;
using System.Threading.Tasks;
using Individuals.Shared;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Individuals.Decorators.Decorators
{
    public class CachingDecorator<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TResult : Result
        
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CachingDecorator<TRequest, TResult>> _logger;
        private string _cacheKey = "_defaultCacheKey";
        public CachingDecorator(IMemoryCache memoryCache, ILogger<CachingDecorator<TRequest, TResult>> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }


        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            var cacheKey = _cacheKey + ReflectObjectProperties(request);
            if (_memoryCache.TryGetValue(cacheKey, out TResult _cachedResponse))

                return _cachedResponse ;

            var result = await next();

            try
            {
                _cachedResponse = result;
                _memoryCache.Set(cacheKey, _cachedResponse, TimeSpan.FromMinutes(5));
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "CachingDecorator Exception.");
               return Result.Error(ResultType.InternalServerError,exception.Message,exception) as TResult;
            }

            return _cachedResponse ;
        }



        public static string ReflectObjectProperties(object Object)
        {
            var returnValue = string.Empty;

            var objectType = Object.GetType();
            var properties = objectType.GetProperties();

            foreach (var property in properties)
            {
                returnValue += property.GetValue(Object)?.ToString();
            }

            return returnValue;
        }

    }



}
