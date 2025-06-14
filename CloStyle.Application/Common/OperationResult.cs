using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloStyle.Application.Common
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }
        public string? ErrorMessage { get; private set; }

        private OperationResult(bool success, string? errorMessage = null)
        {
            IsSuccess = success;
            ErrorMessage = errorMessage;
        }

        public static OperationResult Ok()
        {
            return new OperationResult(true);
        }

        public static OperationResult Fail(string errorMessage)
        {
            return new OperationResult(false, errorMessage);
        }
    }

}
