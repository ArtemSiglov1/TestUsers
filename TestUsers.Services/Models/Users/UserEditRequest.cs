using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEditRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public UserEditRequest() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullName"></param>
        public UserEditRequest(int id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
        /// <summary>
        /// 
        /// </summary>
        public  Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
  public string FullName { get; set; }
    }
}
