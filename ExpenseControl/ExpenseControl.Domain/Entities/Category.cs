using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CategoryDescription { get; set; }
        public Purpose Purpose { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
