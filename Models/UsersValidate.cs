using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class UsersValidate
    {
        [Key]
        public long usersid { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("^([a-zA-Z])+$", ErrorMessage = "Names cannot be numbers")]
        public string firstname { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("^([a-zA-Z])+$", ErrorMessage = "Names cannot be numbers")]
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Passwords must be at least 8 characters")]
        public string password { get; set; }

        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The passwords do not match.")]
        public string passconfirm { get; set; }
    }
}