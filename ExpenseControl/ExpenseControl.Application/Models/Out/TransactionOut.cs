using ExpenseControl.Application.Helpers;
using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Models.Out
{
    public class TransactionOut
    {
        public Guid Id { get; set; }
        public string TransactionDescription { get; set; }
        public decimal TransactionValue { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TransactionTypeName => TransactionType.GetDisplayName();
        public Guid PersonId { get; set; }
        public string PersonName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
