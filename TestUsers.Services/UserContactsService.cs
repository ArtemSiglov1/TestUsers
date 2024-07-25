using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestUsers.Data;
using TestUsers.Data.Models;
using TestUsers.Services.Models;
using TestUsers.Services.Models.Users;

namespace TestUsers.Services
{
    public class UserContactsService
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
        /// <summary>
        /// получение всех контактов пользователя
        /// </summary>
        /// <param name="userId">идентиф</param>
        /// <returns>список контактов</returns>
        public async Task<List<UserContactItem>> GetContacts(Guid userId)
        {
            await using var db = new DataContext(_dbContextOptions);
             
            var contacts = await db.UsersContact.Where(u => u.Id == userId).OrderBy(n => n.Name)
                .Select(x => new UserContactItem()
                {
                    Id = x.Id,
                    Name=x.Name,
                    Value = x.Value,
                }).ToListAsync();

            
            return contacts;
        }
        /// <summary>
        /// сохранение контактов 
        /// </summary>
        /// <param name="request">запрос</param>
        /// <returns>сохраняет изменения в бд</returns>
        public async Task SaveContacts(UserContactSaveRequest request)
        {
            await using var db = new DataContext(_dbContextOptions);

            var dbData = await db.UsersContact.ToListAsync(); 
           var contactItem=  dbData.Where(c => c.Id == request.UserId).Select(x => new UserContactItem() { Id=x.Id,Name=x.Name,Value=x.Value}).ToList();

            foreach (var contact in request.Contacts)
            {
               var contactRequest = request.Contacts.Select(c => c.Id).ToList();
                var contactToRemove = dbData.Where(x => !contactRequest.Contains(x.Id)).ToList();
                db.UsersContact.RemoveRange(contactToRemove);
                var contacts=contactItem?.Select(x=>new UserContact { Name=contact.Name, Value=contact.Value }).FirstOrDefault();
                    await db.UsersContact.AddAsync(contacts); 
               
            }
           

            await db.SaveChangesAsync(); 
        }

    }
}
