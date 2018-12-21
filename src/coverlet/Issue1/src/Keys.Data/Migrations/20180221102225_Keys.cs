using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Keys.Data.Migrations
{
    public partial class Keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentKey = table.Column<string>(nullable: false),
                    KeyType = table.Column<int>(nullable: false),
                    Kid = table.Column<Guid>(nullable: false),
                    Uuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keys_ContentKey",
                table: "Keys",
                column: "ContentKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keys_KeyType_Uuid",
                table: "Keys",
                columns: new[] { "KeyType", "Uuid" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keys");
        }
    }
}
