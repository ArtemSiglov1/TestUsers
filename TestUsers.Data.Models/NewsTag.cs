using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Data.Models
{
    public class NewsTag
    {
       public int Id {  get; set; }
       public string Name {  get; set; }=string.Empty;
       public List<NewsTagsRelation> News { get; set; } = new List<NewsTagsRelation>();

    }
}
