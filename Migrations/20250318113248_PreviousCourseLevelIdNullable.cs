using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilApp.Migrations
{
    /// <inheritdoc />
    public partial class PreviousCourseLevelIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLevels_CourseLevels_PreviousCourseLevelId",
                table: "CourseLevels");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousCourseLevelId",
                table: "CourseLevels",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLevels_CourseLevels_PreviousCourseLevelId",
                table: "CourseLevels",
                column: "PreviousCourseLevelId",
                principalTable: "CourseLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLevels_CourseLevels_PreviousCourseLevelId",
                table: "CourseLevels");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousCourseLevelId",
                table: "CourseLevels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLevels_CourseLevels_PreviousCourseLevelId",
                table: "CourseLevels",
                column: "PreviousCourseLevelId",
                principalTable: "CourseLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
