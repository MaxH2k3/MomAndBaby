using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.ProductDto
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? NumberOfProduct { get; set; }

    }
}
