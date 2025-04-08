using BookList.DomainService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Models
{
    public class BookListResponseDto
    {
        public IEnumerable<BookDto> Books { get; set; } = new List<BookDto>();
        public int TotalCount { get; set; }
    }
}
