using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TransactionDescription { get; set; }
        public decimal TransactionValue { get; set; }
        public TransactionType TransactionType { get; set; }
        public Guid PersonId { get; set; }
        public Guid CategoryId { get; set; }

        public Person Person { get; set; }
        public Category Category { get; set; }
    }
}
