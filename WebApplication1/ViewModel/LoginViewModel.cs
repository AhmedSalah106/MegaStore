using System.ComponentModel.DataAnnotations;

namespace MegaStore.ViewModel
{
    public class LoginViewModel
    {
        [MinLength(2),MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberME { get; set; }
    }
}
