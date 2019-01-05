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

        [Required(ErrorMessage ="*Fisrt Name required")]
        [StringLength(150)]        
        public string FirstName { get; set; }

        [StringLength(150)]        
        public string LastName { get; set; }

        [Required(ErrorMessage = "*User Name required")]
        [StringLength(150)]        
        public string UserName { get; set; }

        [Required(ErrorMessage = "*Password required")]
        [StringLength(50)]        
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        
    }
}
