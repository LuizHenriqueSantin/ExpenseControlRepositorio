using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Application.Models.Helpers
{
    public class EntitySelect
    {
        public string Label { get; set; }
        public Guid Value { get; set; }
        public Infos Infos { get; set; }
    }

    public class Infos
    {
        public int Age { get; set; }
    }
}
