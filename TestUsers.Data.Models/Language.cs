namespace TestUsers.Data.Models
{
    /// <summary>
    /// таблица языков
    /// </summary>
    public class Language
    {
        /// <summary>
        /// идентифик
        /// </summary>
        public int? Id { get; set; }
        public string Code { get; set; } = string.Empty;//ru, en
        public string Name { get; set; } = string.Empty;//Russian, English
        public List<UserLanguage>? Users { get; set; }//пользователь кому присвоить 
    }
}
