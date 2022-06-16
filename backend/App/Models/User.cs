
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.App.Models
{
    [Index(nameof(UserName),IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string FatherName { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string FamilyName { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Occupation { get; set; }
        
        [Required]
        public string BirthDate { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

    }
}
