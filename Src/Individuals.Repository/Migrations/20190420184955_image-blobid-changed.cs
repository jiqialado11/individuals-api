using Microsoft.EntityFrameworkCore.Migrations;

namespace Individuals.Persistance.Migrations
{
    public partial class imageblobidchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageBlobId",
                table: "Individuals",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImageBlobId",
                table: "Individuals",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
