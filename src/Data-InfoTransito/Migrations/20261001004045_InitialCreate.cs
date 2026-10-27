using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_InfoTransito.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sinistros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InjuredPeople = table.Column<bool>(type: "bit", nullable: false),
                    SinistroType = table.Column<int>(type: "int", nullable: false),
                    RoadPavementType = table.Column<int>(type: "int", nullable: false),
                    RoadType = table.Column<int>(type: "int", nullable: false),
                    GroundType = table.Column<int>(type: "int", nullable: false),
                    Weather = table.Column<int>(type: "int", nullable: false),
                    SinistroDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinistros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SinistroID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    CPF = table.Column<string>(type: "varchar(13)", nullable: false),
                    RG = table.Column<string>(type: "varchar(10)", nullable: false),
                    CNH = table.Column<string>(type: "varchar(13)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Sinistros_SinistroID",
                        column: x => x.SinistroID,
                        principalTable: "Sinistros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SinistroExcludeSolicitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SinistroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "SinistrosAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SinistroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Road = table.Column<string>(type: "varchar(200)", nullable: false),
                    Number = table.Column<string>(type: "varchar(50)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(250)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    District = table.Column<string>(type: "varchar(100)", nullable: false),
                    City = table.Column<string>(type: "varchar(200)", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinistrosAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinistrosAddresses_Sinistros_SinistroId",
                        column: x => x.SinistroId,
                        principalTable: "Sinistros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SinistroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarLisencePlate = table.Column<string>(type: "varchar(7)", nullable: false),
                    Brand = table.Column<string>(type: "varchar(50)", nullable: false),
                    Model = table.Column<string>(type: "varchar(50)", nullable: false),
                    ManufacturingYear = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "varchar(50)", nullable: false),
                    TypeVehicle = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Sinistros_SinistroId",
                        column: x => x.SinistroId,
                        principalTable: "Sinistros",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonsAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Road = table.Column<string>(type: "varchar(200)", nullable: false),
                    Number = table.Column<string>(type: "varchar(50)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(250)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    District = table.Column<string>(type: "varchar(100)", nullable: false),
                    City = table.Column<string>(type: "varchar(200)", nullable: false),
                    State = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonsAddresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SinistroID",
                table: "Persons",
                column: "SinistroID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsAddresses_PersonId",
                table: "PersonsAddresses",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SinistroExcludeSolicitation_SinistroId",
                table: "SinistroExcludeSolicitation",
                column: "SinistroId");

            migrationBuilder.CreateIndex(
                name: "IX_SinistrosAddresses_SinistroId",
                table: "SinistrosAddresses",
                column: "SinistroId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_SinistroId",
                table: "Vehicles",
                column: "SinistroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonsAddresses");

            migrationBuilder.DropTable(
                name: "SinistroExcludeSolicitation");

            migrationBuilder.DropTable(
                name: "SinistrosAddresses");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Sinistros");
        }
    }
}
