using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleMvcApp.Models
{
    public class BalanceViewModel
    {
        public IEnumerable<Account> Accounts;
        public string NetWorth => Accounts.Sum(x => x.AccountBalance).ToString("C");
        public string Name { get; set; }
    }

    public class Account
    {
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string Balance => AccountBalance.ToString("C");
        public string Bsb { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Auth0_Ref { get; set; }
        public string Email { get; set; }

        public DateTime LastSeen { get; set; }

        public int Id;

        public string FullName => $"{FirstName} {LastName}";
    }
}
