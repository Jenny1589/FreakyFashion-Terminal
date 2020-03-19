using System;
using System.Collections.Generic;

namespace FreakyFashionTerminal.Models
{
    class Product : IConsoleListable
    {

        public int Id { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;

                UrlSlug = _name
                    .Replace(' ', '-')
                    .ToLower() + $"-{Guid.NewGuid()}";
            }
        }

        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Uri ImageUri { get; set; }
        public string UrlSlug { get; private set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }

        public string ToListItem()
        {
            return Id.ToString().PadRight(10) + Name;
        }
    }
}
