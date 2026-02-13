using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPeminjamanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peminjamans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RuanganId = table.Column<int>(type: "integer", nullable: false),
                    NamaPeminjam = table.Column<string>(type: "text", nullable: false),
                    TanggalPinjam = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Keperluan = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjamans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peminjamans_Ruangans_RuanganId",
                        column: x => x.RuanganId,
                        principalTable: "Ruangans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Peminjamans",
                columns: new[] { "Id", "Keperluan", "NamaPeminjam", "RuanganId", "Status", "TanggalPinjam" },
                values: new object[,]
                {
                    { 1, "Koordinasi Proyek PBL", "Diaz", 1, "Menunggu", new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Seminar Teknologi Industri", "Rina", 2, "Disetujui", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peminjamans_RuanganId",
                table: "Peminjamans",
                column: "RuanganId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peminjamans");
        }
    }
}
