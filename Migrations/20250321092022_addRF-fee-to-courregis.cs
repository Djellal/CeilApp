﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilApp.Migrations
{
    /// <inheritdoc />
    public partial class addRFfeetocourregis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaidFeeValue",
                table: "CourseRegistrations",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "RegistrationTermsAccepted",
                table: "CourseRegistrations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidFeeValue",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "RegistrationTermsAccepted",
                table: "CourseRegistrations");
        }
    }
}
