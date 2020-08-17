using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLibrary.Data.EF.Migrations
{
    public partial class Initilas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Author = table.Column<string>(maxLength: 150, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: false),
                    Body = table.Column<string>(maxLength: 10000, nullable: false),
                    CoverImageName = table.Column<string>(maxLength: 50, nullable: false),
                    ReleaseYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
