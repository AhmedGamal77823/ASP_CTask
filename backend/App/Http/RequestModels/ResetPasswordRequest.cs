using System.ComponentModel.DataAnnotations;

namespace backend.App.Http.RequestModels
{
    public class ResetPasswordRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Should Contain 1 Capital, 1 Small and 1 digit")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Should Contain 1 Capital, 1 Small and 1 digit")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
