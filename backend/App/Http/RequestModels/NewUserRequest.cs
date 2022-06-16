using System.ComponentModel.DataAnnotations;

namespace backend.App.Http.RequestModels
{
    public class NewUserRequest
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string FamilyName { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Should Contain 1 Capital, 1 Small and 1 digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Occupation { get; set; }
    }
}
