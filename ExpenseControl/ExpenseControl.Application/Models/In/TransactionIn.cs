using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Models.In
{
    public class TransactionIn
    {
        public Guid Id { get; set; }
        public string TransactionDescription { get; set; }
        public decimal TransactionValue { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
