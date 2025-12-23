using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Domain.Enums
{
    public enum Purpose
    {
        [Display(Name = "DESPESA")]
        EXPENSE = 1,

        [Display(Name = "RECEITA")]
        INCOME = 2,

        [Display(Name = "AMBAS")]
        BOTH = 3,
    }
}
