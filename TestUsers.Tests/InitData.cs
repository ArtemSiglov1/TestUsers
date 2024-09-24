using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Data.Models;

namespace TestUsers.Tests

{
    public class InitToTable
    {
        public static User InitUser()
        {
            var id = new Guid("00000000-0000-0000-0000-000000000000");
            var api = new User()
            {
                Id = id,

                FullName = "Test",
                DateRegister = DateTime.Now,
                Contacts =
                [ new UserContact()
                {

                    Name="tg",
                    UserId=id,
                    Value="+37377846092",
                },
                new UserContact()
                {
                    Name="Ok",
                    UserId =id,
                    Value="pensia"
                }
                ],
                Email = "dddd",
                News = [ new News {DateCreated=DateTime.UtcNow,
                AuthorId=1,
                Description="Test",
                Title="Test",

                Tags=
                [
                    new NewsTagsRelation()
                    {
                        NewsTagId=1,
                        NewsTag=new NewsTag()
                        {
                            Name = "Test",
                        }
                    }
                ]

                }
            ],
                PasswordHash = "43434",
                Status = Data.Enums.EnumUserStatus.Active,
                UserLanguages =
                [ new UserLanguage

                    {
                    LanguageId=1,
                    DateLearn=DateTime.UtcNow,
                    Language=new Language
                        {
                        Code="35",
                        Name="English"
                        }
                    },
                new UserLanguage{
                    DateLearn= DateTime.UtcNow,
                    LanguageId=2,
                }
            ],

            };


            return api;
        }
        public static Product InitProduct()
        {
            var api = new Product
            {
                Amount = 1000,
                CategoryId = 1,
                DateCreated = DateTime.UtcNow,
                Description = "Test",
                Name = "tests",
                ProductCategoryParameterValueProduct =
        [
            new ProductCategoryParameterValueProduct
            {
                ProductCategoryParameterValueId = 1
            }
        ],
                ProductCategory = new ProductCategory
                {
                    Id = 1,
                    Name = "Root 1",
                    ParentCategoryId = null,
                    ChildCategories =
            [
                new ProductCategory
                {
                    Id = 2,
                    Name = "Child 1.1",
                    ParentCategoryId = 1, },


                        new ProductCategory{
                             Id = 3,
                    Name = "Child 1.2",
                    ParentCategoryId = 1, },
            ]
                }
            };
            
            return api;
        }
    }
}

