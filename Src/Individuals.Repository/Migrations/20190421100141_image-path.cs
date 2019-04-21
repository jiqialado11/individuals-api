using Microsoft.EntityFrameworkCore.Migrations;

namespace Individuals.Persistance.Migrations
{
    public partial class imagepath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBlobId",
                table: "Individuals",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Individuals",
                newName: "ImageBlobId");
        }
    }
}
