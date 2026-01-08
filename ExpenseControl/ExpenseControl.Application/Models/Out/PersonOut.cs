using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Models.Out
{
    public class PersonOut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double? TotalValueExpense { get; set; }
        public double? TotalValueIncome { get; set; }
        public double? TotalValue => TotalValueIncome - TotalValueExpense;
    }
}
