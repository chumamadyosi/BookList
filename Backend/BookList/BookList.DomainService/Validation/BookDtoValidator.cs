using BookList.DomainService.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Validation
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {

        public BookDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.AuthorId)
                .NotEmpty();

        }
    }
}

