using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.Data.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
