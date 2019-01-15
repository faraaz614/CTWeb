using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace CT.Common.Common
{
    public class BaseUserEntity : BaseEntity
    {
        public BaseUserEntity()
        {
            ListUsers = new List<UserEntity>();
        }

        public UserEntity UserEntity { get; set; }
        public List<UserEntity> ListUsers { get; set; }
    }
    public class UserEntity : BaseEntity
    {
        public long ID { get; set; }

        public int RoleID { get; set; }

        [StringLength(500)]
        public string ProfilePic { get; set; }

        [Required(ErrorMessage = "FirstName is required.")]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "FirstName should be 6 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "LastName should be 6 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "UserName should be 6 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50)]
        [MinLength(6, ErrorMessage = "Password should be 6 characters")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string AppImageUrl
        {
            get => ConfigurationManager.AppSettings["imagebase"] + ProfilePic;
        }
    }
}
