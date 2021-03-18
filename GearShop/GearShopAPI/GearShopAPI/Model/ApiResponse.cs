using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearShopAPI.Model
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(bool success)
        {
            this.Success = success;
        }
        public ApiResponse(bool success, object content, string Message)
        {
            this.Success = success;
            this.Message = Message;
            this.Content = content;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
    }
}
