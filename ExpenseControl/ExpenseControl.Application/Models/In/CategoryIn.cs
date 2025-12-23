using ExpenseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Models.In
{
    public class CategoryIn
    {
        public Guid Id { get; set; }
        public string CategoryDescription { get; set; }
        public Purpose Purpose { get; set; }
    }
}
