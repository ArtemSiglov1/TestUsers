using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 
        /// </summary>
       public bool IsSuccess { get; set; }
  /// <summary>
  /// 
  /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseResponse() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMessage"></param>
        public BaseResponse(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}
