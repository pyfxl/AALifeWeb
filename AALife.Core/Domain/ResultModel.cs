using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain
{
    public class ResultModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public ResultModel()
        {

        }

        public ResultModel(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
    }
}
