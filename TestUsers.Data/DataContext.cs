using Microsoft.EntityFrameworkCore;
using TestUsers.Data.Models;
using TestUsers.Data.Enums;
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



            base.OnModelCreating(modelBuilder);
        }
    }
}