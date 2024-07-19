using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestUsers.Data.Enums;

namespace TestUsers.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateRegister { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EnumUserStatus Status { get; set; }
    }
}