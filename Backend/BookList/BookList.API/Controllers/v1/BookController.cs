using BookList.DomainService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookList.DomainService.Book;
using Microsoft.AspNetCore.Authorization;
//using BookList.DomainService.Response;
using BookList.DomainService.Enums;
using BookList.DomainService.Models;

namespace BookList.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<BookListResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<BookListResponseDto>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<BookListResponseDto>>> GetBooks(CancellationToken cancellationToken,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _bookService.GetBooksPaginatedAsync(pageNumber, pageSize, cancellationToken);

                return Ok(new ApiResponse<BookListResponseDto>().OK(result!));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<PaginatedResponse<BookDto>>().Error((int)ErrorCode.InternalServerError));
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<BookDto>>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new ApiResponse<BookDto>().Error((int)ErrorCode.BookNotFound));
            }
            return Ok(new ApiResponse<BookDto>().OK(book));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<BookDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<BookDto>>> CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest(new ApiResponse<BookDto>().Error((int)ErrorCode.InvalidRequest));
            }

            await _bookService.AddBookAsync(bookDto);
            var response = new ApiResponse<BookDto>().OK(bookDto);
            return CreatedAtAction(nameof(GetBook), new { id = bookDto.Id }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest(ApiResponse.Error((int)ErrorCode.InvalidRequest));
            }

            var existingBook = await _bookService.GetBookByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound(ApiResponse.Error((int)ErrorCode.BookNotFound));
            }

            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(ApiResponse.Error((int)ErrorCode.BookNotFound));
            }

            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
    }
}
