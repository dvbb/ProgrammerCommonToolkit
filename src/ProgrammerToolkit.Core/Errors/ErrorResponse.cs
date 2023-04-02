using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerToolkit.Core.Errors
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors=new List<ErrorInfo>();
        }
        public ErrorResponse(ErrorInfo errorInfo)
        {
            Errors.Add(errorInfo);
        }
        public ErrorResponse(List<ErrorInfo> errors)
        {
            Errors.AddRange(errors);
        }

        public List<ErrorInfo> Errors = new List<ErrorInfo>();
    }
}
