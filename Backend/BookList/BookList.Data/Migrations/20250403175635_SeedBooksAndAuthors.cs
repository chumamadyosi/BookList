using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookList.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooksAndAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "George R. R. Martin" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "Description", "ISBN", "PublishedYear", "Title" },
                values: new object[,]
                {
                    { 1, 1, "The first book in the Harry Potter series.", "9780747532743", 1997, "Harry Potter and the Philosopher's Stone" },
                    { 2, 2, "The first book in the A Song of Ice and Fire series.", "9780553103540", 1996, "A Game of Thrones" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 2);
        }
    }
}
