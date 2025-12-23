using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
