using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerToolkit.Core.Errors
{
    public class ErrorMapBase: IErrorMap
    {
        static Dictionary<int,string> errorMap { get; set; }
        static void AddMapping(int errorCode,string message)
        {
            errorMap.Add(errorCode, message);
        }
        static ErrorMapBase()
        {
            errorMap = new Dictionary<int, string>();
            AddMapping(ErrorCode.JWT_DECODE_ERROR, "Jwt token format error.");
            AddMapping(ErrorCode.JWT_NULL_ERROR, "Jwt token cannot be null.");
        }
        public ErrorInfo GetErrorInfo(int errorCode)
        {
            if (errorMap.ContainsKey(errorCode))
            {
                return new ErrorInfo
                {
                    ErrorCode = errorCode,
                    Message = errorMap[errorCode]
                };
            }
            throw new ArgumentOutOfRangeException();
        }
        public ErrorResponse CreateErrorResponse(int errorCode)
        {
            var errorInfo = GetErrorInfo(errorCode);
            return new ErrorResponse(errorInfo);
        }
    }
}
