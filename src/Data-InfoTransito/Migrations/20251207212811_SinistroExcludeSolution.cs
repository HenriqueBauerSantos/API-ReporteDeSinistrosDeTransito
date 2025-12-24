using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_InfoTransito.Migrations
{
    /// <inheritdoc />
    public partial class SinistroExcludeSolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SinistroExcludeSolicitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SinistroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Motive = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AnalyzedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnalyzedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinistroExcludeSolicitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinistroExcludeSolicitation_Sinistros_SinistroId",
                        column: x => x.SinistroId,
                        principalTable: "Sinistros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinistroExcludeSolicitation_SinistroId",
                table: "SinistroExcludeSolicitation",
                column: "SinistroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinistroExcludeSolicitation");
        }
    }
}
