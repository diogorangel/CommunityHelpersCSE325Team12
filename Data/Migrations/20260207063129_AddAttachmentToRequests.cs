using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityHelpers.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentToRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentPath",
                table: "HelpRequests",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentPath",
                table: "HelpRequests");
        }
    }
}
