using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Message == null || Message.Count == 0;

        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore]
        public string? urlAsCreated { get; set; }

        //Static Factory Method

        public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK )
        {
            return new ServiceResult<T> { Data = data, StatusCode=status };
        }

        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T> { Data = data, urlAsCreated = url };
        }

        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest) 
        {
            return new ServiceResult<T>() { Message = errorMessage, StatusCode=status };
        }

        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>() { Message = [errorMessage], StatusCode = status };
        }
    }

    public class ServiceResult
    {
        public List<string>? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Message == null || Message.Count == 0;

        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        //Static Factory Method

        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult{ StatusCode = status };
        }

        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult() { Message = errorMessage, StatusCode = status };
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult() { Message = [errorMessage], StatusCode = status };
        }
    }
}
