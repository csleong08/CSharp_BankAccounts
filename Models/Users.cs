using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Users
    {
        [Key]
        public long usersid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public Users()
        {
            Transaction = new List<Transactions>();
        }
        public List<Transactions> Transaction;
    }
}