using BookList.DomainService.DTOs;
using BookList.DomainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Book
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto?> GetBookByTitleAsync(string title);
        Task AddBookAsync(BookDto bookDto);
        Task UpdateBookAsync(BookDto bookDto);
        Task DeleteBookAsync(int id);
        // Task<IEnumerable<BookDto>> GetBooksAsync(int pageNumber, int pageSize);
        //Task<(IEnumerable<BookDto> Books, int TotalCount)> GetBooksPaginatedAsync(int pageNumber, int pageSize);
        Task<BookListResponseDto> GetBooksPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellation);
    }
}
