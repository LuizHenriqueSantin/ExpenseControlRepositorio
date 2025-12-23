using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Domain.Enums
{
    public enum TransactionType
    {
        [Display(Name = "DESPESA")]
        EXPENSE = 1,

        [Display(Name = "RECEITA")]
        INCOME = 2,
    }
}
