using BookList.Data.Entities;
using BookList.Data.Repository;
using BookList.DomainService.DTOs;
using BookList.DomainService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.DomainService.Book
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllWithAuthorsAsync();
            return books.Select(b => new BookDto
            {
                Id = b.BookId,
                Title = b.Title,
                AuthorId = b.AuthorId,
                AuthorName = b.Author.Name,
                PublishedYear = b.PublishedYear,
                ISBN = b.ISBN,
                Description = b.Description
            });
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return book == null ? null : new BookDto
            {
                Id = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublishedYear = book.PublishedYear,
                ISBN = book.ISBN,
                Description = book.Description
            };
        }

        public async Task<BookDto?> GetBookByTitleAsync(string title)
        {
            var book = await _bookRepository.GetBookByTitleAsync(title);
            return book == null ? null : new BookDto
            {
                Id = book.BookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.Name,
                PublishedYear = book.PublishedYear,
                ISBN = book.ISBN,
                Description = book.Description
            };
        }
        public async Task AddBookAsync(BookDto bookDto)
        {
            var book = new Data.Entities.Book
            {
                Title = bookDto.Title,
                AuthorId = bookDto.AuthorId,
                PublishedYear = bookDto.PublishedYear,
                ISBN = bookDto.ISBN,
                Description = bookDto.Description,
            };
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(BookDto bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(bookDto.Id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.AuthorId = bookDto.AuthorId;
                book.PublishedYear = bookDto.PublishedYear;
                book.ISBN = bookDto.ISBN;
                book.Description = bookDto.Description;
                await _bookRepository.Update(book);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
               await _bookRepository.Delete(book);
            }
        }
        public async Task<BookListResponseDto> GetBooksPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellation)
        {
            var (books, totalCount) = await _bookRepository.GetBooksPaginatedAsync(pageNumber, pageSize, cancellation);

            var dtoList = books.Select(b => new BookDto
            {
                Id = b.BookId,
                Title = b.Title,
                AuthorId = b.AuthorId,
                AuthorName = b.Author?.Name ?? "Unknown",
                PublishedYear = b.PublishedYear,
                ISBN = b.ISBN,
                Description = b.Description
            }).ToList(); 

            return new BookListResponseDto
            {
                Books = dtoList,
                TotalCount = totalCount
            };
        }
    }
}
