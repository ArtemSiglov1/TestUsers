using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services
{
    internal class UserContactsService
    {
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// конструктор с параметраи 
        /// </summary>
        /// <param name="dbContextOptions">опции работы с данными</param>
        public UserContactsService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        public async Task<List<UserContactItem>> GetContacts(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);


            var contacts = await db.UsersContact.OrderBy(n => n.Name)
                .Select(x => new UserContactItem()
                {
                    Id = x.Id,
                    Name=x.Name,
                    Value = x.Value,
                })
    .ToListAsync();

            
            return contacts;
        }
      public async Task SaveContacts(UserContactSaveRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);
            var query = db.UsersContact.AsQueryable();
            var user = await query.FirstOrDefaultAsync(x => x.UserId == request.UserId);
            if (user == null)
            {
                return;
            }
            if(string.IsNullOrWhiteSpace(user.Name))
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(user.Value))
            {
                return;
            }
            user.UserId = request.UserId;
            user.Name= request.Contacts.FirstOrDefault(x=>x.Id==user.Id).Name;
            user.Value= request.Contacts.FirstOrDefault(x => x.Id == user.Id).Value;
            db.Entry(user).State = EntityState.Modified;
        }

    }
}
