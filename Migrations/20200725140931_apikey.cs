using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace captcha.Migrations
{
    public partial class apikey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApiKeyId",
                table: "SessionCaptchas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionCaptchas_ApiKeyId",
                table: "SessionCaptchas",
                column: "ApiKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionCaptchas_ApiKeys_ApiKeyId",
                table: "SessionCaptchas",
                column: "ApiKeyId",
                principalTable: "ApiKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionCaptchas_ApiKeys_ApiKeyId",
                table: "SessionCaptchas");

            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropIndex(
                name: "IX_SessionCaptchas_ApiKeyId",
                table: "SessionCaptchas");

            migrationBuilder.DropColumn(
                name: "ApiKeyId",
                table: "SessionCaptchas");
        }
    }
}
