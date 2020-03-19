using System;

namespace FreakyFashionTerminal.Models
{
    class Category : IConsoleListable
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

        public Uri ImageUri { get; set; }
        public string UrlSlug { get; private set; }

        public string ToListItem()
        {
            return Id.ToString().PadRight(10) + Name;
        }
    }
}
