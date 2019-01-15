using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain
{
    public class ResultApiModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }
        
        public string Data { get; set; }

        public ResultApiModel()
        {
            this.Success = true;
            this.Message = "成功。";
            this.Data = "";
        }

        public ResultApiModel(bool success, string message, string data)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }
    }
}
