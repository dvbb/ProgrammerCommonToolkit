using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerToolkit.Core.Errors
{
    public interface IErrorMap
    {
        ErrorInfo GetErrorInfo(int errorCode);
        ErrorResponse CreateErrorResponse(int errorCode);
    }
}
