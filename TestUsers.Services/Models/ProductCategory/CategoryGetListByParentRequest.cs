using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Services.Models.ProductCategory
{
    public class CategoryGetListByParentRequest
    {
        public int? ParentCategoryId { get; set; }// -- ид родительской категории, или если нулл - то корневые категории
        public string? Search { get; set; }

    }
}
