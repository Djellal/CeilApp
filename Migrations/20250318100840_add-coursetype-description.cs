using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilApp.Migrations
{
    /// <inheritdoc />
    public partial class addcoursetypedescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CourseTypes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CourseTypes");
        }
    }
}
