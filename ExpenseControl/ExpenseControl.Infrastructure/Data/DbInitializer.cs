using ExpenseControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControl.Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@admin.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
