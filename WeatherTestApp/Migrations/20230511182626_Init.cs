using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherTestApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherPeriods",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Humidity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cloudiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloudBase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorizontalVisibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeatherConditions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherPeriods", x => x.Guid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherPeriods");
        }
    }
}
