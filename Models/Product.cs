using System;
using System.Collections.Generic;

namespace FreakyFashionTerminal.Models
{
    class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Uri ImageUri { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
