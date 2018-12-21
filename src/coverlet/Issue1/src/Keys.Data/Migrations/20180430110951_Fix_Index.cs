using Microsoft.EntityFrameworkCore.Migrations;

namespace Keys.Data.Migrations
{
    public partial class Fix_Index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Keys_ContentKey",
                table: "Keys");

            migrationBuilder.AlterColumn<string>(
                name: "ContentKey",
                table: "Keys",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Keys_KeyType_Kid",
                table: "Keys",
                columns: new[] { "KeyType", "Kid" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Keys_KeyType_Kid",
                table: "Keys");

            migrationBuilder.AlterColumn<string>(
                name: "ContentKey",
                table: "Keys",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Keys_ContentKey",
                table: "Keys",
                column: "ContentKey",
                unique: true);
        }
    }
}
