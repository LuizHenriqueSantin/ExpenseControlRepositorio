using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Models.Out
{
    public class CategoryOut
    {
        public Guid Id { get; set; }
        public string CategoryDescription { get; set; }
        public Purpose Purpose { get; set; }
        public string PurposeName { get; set; }
        public double? TotalValueExpense { get; set; }
        public double? TotalValueIncome { get; set; }
        public double? TotalValue => TotalValueIncome - TotalValueExpense;
    }
}
