﻿using System.ComponentModel.DataAnnotations;

namespace backend.App.Http.RequestModels
{
    public class GetUsersRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The password must be at least {8} and at max {32} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Should Contain 1 Capital, 1 Small and 1 digit")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
