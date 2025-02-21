using System.ComponentModel.DataAnnotations;

namespace MegaMarket.ViewModel
{
    public class RegisterViewModel
    {
        [MinLength(2),MaxLength(50)]
        public string Name { get; set; }
        [MinLength(4),MaxLength(50)]
        public string Password {  get; set; }
        [Compare("Password")]
        public string ConfirmPassword {  get; set; }

        public bool IsVendor { get; set; } 
        public bool IsSeller { get; set; }

        [Phone]
        public string? Phone { get; set; }
    }
}
