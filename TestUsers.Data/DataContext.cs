using Microsoft.EntityFrameworkCore;
using TestUsers.Data.Models;
using TestUsers.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace TestUsers.Data
{
   /// <summary>
   /// класс реализующий работу с бд 
   /// </summary>
    public class DataContext:DbContext
    {
       /// <summary>
       /// указание на то что будет таблица пользователи
       /// </summary>
        public DbSet<User> Users { get; set; }
       public DbSet<UserContact> UsersContact { get; set; }
       public  DbSet<UserLanguage> UsersLanguage { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }
        public DbSet<NewsTagsRelation> NewsTagsRelations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryParameter> ProductCategoryParameters { get; set; }
        public DbSet<ProductCategoryParameterValue> ProductCategoriesParameterValue { get; set; }
        public DbSet<ProductCategoryParameterValueProduct> ProductCategoryParameterValueProducts { get; set; }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public DataContext()
        {
            //Database.EnsureCreated();
        }
        /// <summary>
        /// конструктор с параметром 
        /// </summary>
        /// <param name="options">опции работы с бд</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
        /// <summary>
        /// конфиг бд
        /// </summary>
        /// <param name="optionsBuilder">способ постройки</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Users;Username=postgres;Password=111111");
        }
        /// <summary>
        /// модель создания
        /// </summary>
        /// <param name="modelBuilder">модель постройки </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            //modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(100);
            modelBuilder.Entity<NewsTagsRelation>()
       .HasKey(ntr => new { ntr.NewsId, ntr.NewsTagId });
       //     modelBuilder.Entity<NewsTagsRelation>()
       //.HasNoKey();
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductCategory>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductCategoryParameter>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductCategoryParameterValue>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductCategoryParameterValueProduct>().HasKey(x => x.ProductId);
            modelBuilder.Entity<UserLanguage>().HasKey(x => x.Id);
            // modelBuilder.Entity<UserContact>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}