using FluentValidation;
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
using TestUsers.Services.Validators.UserContactValid;

namespace TestUsers.Services.Services
{
    public class UserContactsService
    {
       readonly private DbContextOptions<DataContext> _dbContextOptions;
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

            var contacts = await db.UsersContact.Where(u => u.UserId == userId).OrderBy(n => n.Name)
                .Select(x => new UserContactItem()
                {
                    Id = x.Id,
                    Name = x.Name,
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
            var valid = new SaveContactValidator();
            await valid.ValidateAndThrowAsync(request);

            await using var db = new DataContext(_dbContextOptions);

            var existingContacts = await db.UsersContact
                .Where(c => c.UserId == request.UserId)
                .ToListAsync();

            var requestContactIds = request.Contacts.Select(c => c.Id).ToList();

            var contactsToRemove = existingContacts
                .Where(c => !requestContactIds.Contains(c.Id))
                .ToList();

            db.UsersContact.RemoveRange(contactsToRemove);

            foreach (var contactRequest in request.Contacts)
            {
                var existingContact = existingContacts.FirstOrDefault(c => c.Id == contactRequest.Id);

                if (existingContact != null)
                {
                    existingContact.Name = contactRequest.Name;
                    existingContact.Value = contactRequest.Value;
                }
                else
                {
                    var newContact = new UserContact
                    {
                        Id = contactRequest.Id,
                        UserId = request.UserId,
                        Name = contactRequest.Name,
                        Value = contactRequest.Value
                    };
                    await db.UsersContact.AddAsync(newContact);
                }
            }

            await db.SaveChangesAsync();
        }


    }
}
