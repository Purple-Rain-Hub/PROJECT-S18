using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateEfCoreIdentity.Migrations
{
    /// <inheritdoc />
    public partial class NumberUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Number",
                table: "Rooms");
        }
    }
}
