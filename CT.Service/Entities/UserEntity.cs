using System.ComponentModel.DataAnnotations;

namespace CT.Service.Entities
{
    public class BaseUserEntity : BaseEntity
    {
        public UserEntity UserEntity { get; set; }
    }
    public class UserEntity
    {
        public long ID { get; set; }

        public int? RoleID { get; set; }

        [StringLength(500)]
        public string ProfilePic { get; set; }

        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 3 characters")]
        public string FirstName { get; set; }

        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 3 characters")]
        public string LastName { get; set; }

        [StringLength(150)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string UserName { get; set; }

        [StringLength(50)]
        [MinLength(6, ErrorMessage = "Minimum length should be 6 characters")]
        public string Password { get; set; }
    }
}
