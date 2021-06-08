using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace task7
{
    public partial class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public int Year { get; set; }
        public string Language { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
