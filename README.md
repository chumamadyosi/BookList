📚 Book Management System
A full-stack Book Management application built with ASP.NET Core and React, implementing modern best practices such as JWT authentication, SOLID principles, repository pattern, and fluent validation.
________________________________________
🔧 Technologies Used
Backend
•	ASP.NET Core Web API
•	Entity Framework Core (Code First + Migrations)
•	JWT Authentication
•	SQL Server with indexed tables
•	FluentValidation (in progress)
•	Custom Middleware for Exception Handling
•	Logging
•	SOLID Principles
•	Generic Repository Pattern
•	Custom API Response Wrappers
•	Dependency Injection
•	CancellationToken Support
Frontend
•	React (with TypeScript)
•	Material UI (MUI)
•	Axios for HTTP requests
•	JWT Authentication
•	Responsive UI
•	Book Listing, Search, Pagination, Add, Edit, Delete
________________________________________
✅ Features Implemented
🛠 Backend Functionality
•	Database Design
o	Authors, Books, Users tables
o	Relationships and indexing for performance
•	Authentication & Authorization
o	JWT-based authentication
o	Login system
o	Role-based authorization (expandable)
o	Secure password hashing
•	Controllers
o	AuthenticationController – login endpoint with JWT response
o	BooksController – full CRUD + search + pagination
o	UsersController – user management endpoints (if exposed)
•	Services
o	BookService – handles business logic for books
o	AuthorService – manages authors
o	AuthenticationService – login logic and token generation
o	UserService – user creation and management
•	Validation & Error Handling
o	Custom ExceptionMiddleware for clean API errors
o	Centralized list of error codes (via ErrorCodes enum)
o	FluentValidation (incomplete)
•	Best Practices
o	SOLID architecture
o	Repositories: GenericRepository, BookRepository, UserRepository
o	Separation of concerns
o	Pagination and CancellationToken support
o	Logging at critical points
________________________________________
💻 Frontend Functionality
•	Book List Page
o	View all books with pagination
o	Search by title, author, or ISBN
o	Edit and Delete functionality
o	Responsive layout
•	Book Form
o	Create or update a book
o	Auto-populated fields when editing
o	Cancel and Save buttons
•	Authentication
o	Login screen with JWT token storage
o	Logout support
•	Usability
o	Pagination with state management
o	Search box with debounce (soon to improve UX)
o	Table UI with Material UI components
o	Loading spinners and error messaging
________________________________________
🧪 What’s Missing / To Do
•	✅ Unit Tests
o	AuthenticationService
o	BookService
o	Controller tests
•	🔁 Integration Tests
•	🎯 Code Cleanup & Refactoring
o	Consolidate duplicate logic
o	Better naming and comments
•	🔐 Encryption
o	Use for sensitive fields (optional enhancement)
•	📏 Complete FluentValidation Rules
•	💡 Improve Search UX
o	Debounce API calls to avoid flickering on each keystroke
•	🧑‍🎨 UI Enhancements
o	Toasts/snackbars for feedback
o	Better error visuals
________________________________________
🔐 Authentication Flow
1.	User logs in using email and password.
2.	Backend validates credentials and returns a JWT token.
3.	Token is stored in local storage on the frontend.
4.	Token is sent in Authorization headers for protected endpoints.
🐛 Known Bug – Search Input Refresh /🐞 Bug – Search Field Loses Focus
•	On the Book List page, as you type in the search box, the entire page refreshes to fetch new data.
•	This causes the search box to lose focus, making it feel like it's playing hide and seek with your cursor. 🎯⌨️
•	What's happening? The search triggers a backend call that refreshes the component without preserving input focus.
•	Suggested Fix: Debounce the input, manage search state properly, and avoid full component refresh on each keystroke.
🗂️ Sample Data
•	An extra SQL script for inserting sample data is available in the project’s root directory. Just run it to populate the database for testing.

.

🚀 Getting Started
1.	Backend
cd Backend/
dotnet ef database update
dotnet run
2.	Frontend
cd book-list-app/
npm install
npm start

🔐 Test Login Credentials

- **Username:** `admin`
- **Password:** `Admin@123`

> Use these credentials to log in during local test.

// Note: I'm a backend developer by trade — frontend isn’t my strongest suit, so feedback is welcome! Not  expert, but I do my best. 😊

