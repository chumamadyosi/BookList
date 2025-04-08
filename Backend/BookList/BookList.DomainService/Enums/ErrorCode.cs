using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookList.DomainService.Enums
{
    using System.ComponentModel;

    public enum ErrorCode
    {
        // General Errors
        [Description("Unknown error occurred")]
        UnknownError = 1000,

        [Description("Invalid request")]
        InvalidRequest = 1001,

        [Description("Missing required field")]
        MissingRequiredField = 1002,

        [Description("Invalid field format")]
        InvalidFieldFormat = 1003,

        [Description("Unauthorized access")]
        Unauthorized = 1004,

        [Description("Access forbidden")]
        Forbidden = 1005,

        [Description("Resource not found")]
        NotFound = 1006,

        [Description("Conflict detected")]
        Conflict = 1007,

        [Description("Internal server error")]
        InternalServerError = 1008,

        // Book Errors
        [Description("Book not found")]
        BookNotFound = 2000,

        [Description("Book already exists")]
        BookAlreadyExists = 2001,

        [Description("Book is currently unavailable")]
        BookUnavailable = 2002,

        [Description("Invalid ISBN format")]
        InvalidIsbnFormat = 2003,

        [Description("Book validation error")]
        BookValidationError = 2004,

        // Author Errors
        [Description("Author not found")]
        AuthorNotFound = 3000,

        [Description("Author already exists")]
        AuthorAlreadyExists = 3001,

        [Description("Author validation error")]
        AuthorValidationError = 3002,

        // Category/Genre Errors
        [Description("Category not found")]
        CategoryNotFound = 4000,

        [Description("Category already exists")]
        CategoryAlreadyExists = 4001,

        [Description("Invalid category")]
        InvalidCategory = 4002,

        // User/Access Errors
        [Description("User not found")]
        UserNotFound = 5000,

        [Description("User not authorized")]
        UserNotAuthorized = 5001,

        [Description("Invalid user role")]
        InvalidUserRole = 5002,

        [Description("User quota exceeded")]
        UserQuotaExceeded = 5003,

        // Data/Persistence Errors
        [Description("Database error")]
        DatabaseError = 6000,

        [Description("Data conflict")]
        DataConflict = 6001,

        [Description("Data validation failed")]
        DataValidationFailed = 6002,

        // External Service Errors
        [Description("External service unavailable")]
        ExternalServiceUnavailable = 7000,

        [Description("External service timeout")]
        ExternalServiceTimeout = 7001,

        [Description("External service error")]
        ExternalServiceError = 7002
    }

}
