using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptionWebApplication.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assemblies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeComponents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Step1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Step2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Step3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Step4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Step5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    People1 = table.Column<int>(type: "int", nullable: false),
                    People2 = table.Column<int>(type: "int", nullable: true),
                    People3 = table.Column<int>(type: "int", nullable: true),
                    People4 = table.Column<int>(type: "int", nullable: true),
                    People5 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assemblies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guarentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaultDetection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosticPeople = table.Column<int>(type: "int", nullable: false),
                    ComplectedWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairPeople = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarentes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assemblies");

            migrationBuilder.DropTable(
                name: "Guarentes");
        }
    }
}
