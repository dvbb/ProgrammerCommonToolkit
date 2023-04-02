using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBackEnd.DataTransferModel
{
    public class UserMessageRequest
    {
        public string Message { get; set; }
        public string Role { get; set; }
    }
}
