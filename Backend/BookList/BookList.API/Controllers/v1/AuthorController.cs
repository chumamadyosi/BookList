using BookList.Data.Repository;
using BookList.DomainService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookList.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorRepository authorRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAthours()
        {
            var authors = await authorRepository.GetAllAsync();
            return Ok(authors);

        }
    }
}
