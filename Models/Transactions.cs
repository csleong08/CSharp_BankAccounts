using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Transactions
    {
        [Key]
        public long transactionsid { get; set; }
        [Required]
        [RegularExpression("^([^a-zA-Z])+$", ErrorMessage = "Names should have at least 2 characters")]
        public double amount { get; set; }
        [Required]
        public DateTime updated_at { get; set; }
        [Required]
        public DateTime created_at { get; set; }
        [Required]
        public long usersid { get; set; }
        [Required]
        public Users Users { get; set; }

    }
}