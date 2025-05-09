using MeetingRoomApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomApp.Data.Db
{
    public class DbInitializer
    {
        public static void Seed(dbContext context)
        {
            if (!context.User.Any())
            {
                context.User.Add(new User() { FirstName = "Admin", LastName = "1", Email = "admin@gmail.com", Phone = "123", Gender = true, BirthDate = DateTime.Now, Address = "Jakarta", CreatedTime = DateTime.Now });
            }
            context.SaveChanges();
        }
    }
}
