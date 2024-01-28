using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemApi.Model
{    public class AccountModel
    {
        public string AccountName { get; set; }
        public decimal InitialBalance { get; set; }
        public string AccountId { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal WithdrawalAmount { get; set; }
    }
}