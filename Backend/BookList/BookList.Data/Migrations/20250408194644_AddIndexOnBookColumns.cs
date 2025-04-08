using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookList.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexOnBookColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId_PublishedYear",
                table: "Books",
                columns: new[] { "AuthorId", "PublishedYear" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN_Unique",
                table: "Books",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId_PublishedYear",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ISBN_Unique",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Title",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }
    }
}
