using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Common
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public static ApiResponse<T> FromResult(Result<T> result)
            => new()
            {
                success = result.IsSuccess,
                message = result.Error,
                data = result.Data
            };
    }

}
