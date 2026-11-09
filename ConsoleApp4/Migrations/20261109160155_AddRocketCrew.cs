using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp4.Migrations
{
    /// <inheritdoc />
    public partial class AddRocketCrew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rocket_crew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CrewName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RocketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rocket_crew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rocket_crew_Rockets_RocketId",
                        column: x => x.RocketId,
                        principalTable: "Rockets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_rocket_crew_RocketId",
                table: "rocket_crew",
                column: "RocketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rocket_crew");
        }
    }
}
