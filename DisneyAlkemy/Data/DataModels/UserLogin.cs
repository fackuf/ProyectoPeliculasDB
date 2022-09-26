using System.ComponentModel.DataAnnotations;

namespace DisneyAlkemy.Data.DataModels
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
