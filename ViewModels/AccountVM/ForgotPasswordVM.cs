using System.ComponentModel.DataAnnotations;

namespace EnGlamor.ViewModels.AccountVM
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
