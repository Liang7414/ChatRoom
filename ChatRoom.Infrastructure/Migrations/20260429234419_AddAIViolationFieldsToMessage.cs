using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatRoom.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAIViolationFieldsToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsViolation",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ViolationReason",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViolation",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ViolationReason",
                table: "Messages");
        }
    }
}
