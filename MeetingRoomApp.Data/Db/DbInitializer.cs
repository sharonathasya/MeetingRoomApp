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
            if (!context.Account.Any())
            {
                var user = new User
                {
                    FirstName = "Admin",
                    LastName = "1",
                    Email = "admin@gmail.com",
                    Phone = "123",
                    Gender = true,
                    BirthDate = DateTime.Now,
                    Address = "Jakarta",
                    CreatedTime = DateTime.Now
                };
                context.User.Add(user);
                context.SaveChanges(); // Ensure user gets an Id

                var account = new Account
                {
                    UserId = user.UserId,
                    Username = "admin1",
                    Email = user.Email,
                    Password = "admin123",
                    CreatedTime = DateTime.Now,
                    IsActive = true
                    
                };
                context.Account.Add(account);
                context.SaveChanges();

                var roles = new List<TblMasterRole>
                {
                    new TblMasterRole { RoleName = "admin"  },
                    new TblMasterRole { RoleName = "employee" }
                };
                context.TblMasterRole.AddRange(roles);
                context.SaveChanges();

                var adminRole = roles.FirstOrDefault(r => r.RoleName == "admin");


                var accountRole = new AccountRole
                {
                    AccountId = account.UserId,
                    RoleId = adminRole.Id
                };
                context.AccountRole.AddRange(accountRole);
                context.SaveChanges();
            }

            if (!context.TblSystemParameter.Any())
            {
                context.TblSystemParameter.Add(new TblSystemParameter() { Key = "1", Value = "Pending", Description = "Status Pending for Booking", CreatedTime = DateTime.Now, IsActive = true, IsDeleted = false} );
                context.TblSystemParameter.Add(new TblSystemParameter() { Key = "2", Value = "Approve", Description = "Status Approve for Booking", CreatedTime = DateTime.Now, IsActive = true, IsDeleted = false} );
                context.TblSystemParameter.Add(new TblSystemParameter() { Key = "3", Value = "Reject", Description = "Status RejectS for Booking", CreatedTime = DateTime.Now, IsActive = true, IsDeleted = false} );
                context.SaveChanges();
            }

            if (!context.MeetingRoom.Any() )
            {
                context.MeetingRoom.Add( new MeetingRoom() { RoomName = "Room A", Description = "Room A for Meeing 4 people", CreatedAt = DateTime.Now } );
                context.SaveChanges();
            }

        }
    }
}
