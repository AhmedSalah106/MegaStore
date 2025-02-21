using System.ComponentModel.DataAnnotations;

namespace MegaMarket.ViewModel
{
    public class RoleViewModel
    {
        [MinLength(2),MaxLength(50)]
        public string RoleName { get; set; }
    }
}
