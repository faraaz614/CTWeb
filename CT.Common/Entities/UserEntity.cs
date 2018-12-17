using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [StringLength(500)]
        public string ProfilePic { get; set; }

        [Required]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
