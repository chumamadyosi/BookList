using BookList.DomainService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }

        public static ApiResponse Error(int errorCode)
        {
            return new ApiResponse
            {
                Success = false,
                ErrorCode = errorCode,
                ErrorMessage = EnumHandler.GetEnumDescription((ErrorCode)errorCode)
            };
        }

        public static ApiResponse OK()
        {
            return new ApiResponse
            {
                Success = true
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }

        public new ApiResponse<T> Error(int errorCode)
        {
            return new ApiResponse<T>
            {
                Success = false,
                ErrorCode = errorCode,
                ErrorMessage = EnumHandler.GetEnumDescription((ErrorCode)errorCode)
            };
        }

        public ApiResponse<T> ErrorWithMessage(string message, int errorCode = (int)Enums.ErrorCode.InternalServerError, T? data = default)
        {
            return new ApiResponse<T>
            {
                Success = false,
                ErrorCode = errorCode,
                ErrorMessage = message,
                Data = data
            };
        }

        public ApiResponse<T> OK(T _data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = _data
            };
        }
    }
}
