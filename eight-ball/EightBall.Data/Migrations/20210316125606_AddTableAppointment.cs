using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EightBall.Data.Migrations
{
    public partial class AddTableAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableAppointments",
                columns: table => new
                {
                    AppointmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TablesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableAppointments", x => new { x.AppointmentsId, x.TablesId });
                    table.ForeignKey(
                        name: "FK_TableAppointments_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableAppointments_Tables_TablesId",
                        column: x => x.TablesId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableAppointments_TablesId",
                table: "TableAppointments",
                column: "TablesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableAppointments");
        }
    }
}
