using System.ComponentModel.DataAnnotations;

namespace VHSStore.Domain.Models.UserModels
{
    public class AddUser
    {
        [Required(ErrorMessage = "Missing: UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Missing: Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Missing: Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Missing: Subscribed")]
        public bool Subscribed { get; set; }
    }
}
