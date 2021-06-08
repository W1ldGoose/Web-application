using System;
using System.Collections.Generic;

#nullable disable

namespace task7
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
