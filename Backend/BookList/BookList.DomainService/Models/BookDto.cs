using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }  
        public string AuthorName { get; set; } = string.Empty;
        public int? PublishedYear { get; set; }
        public string? ISBN { get; set; }
        public string? Description { get; set; }
    }
}
