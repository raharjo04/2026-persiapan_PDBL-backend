using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruangans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamaRuangan = table.Column<string>(type: "text", nullable: false),
                    Kapasitas = table.Column<int>(type: "integer", nullable: false),
                    Lokasi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruangans", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ruangans",
                columns: new[] { "Id", "Kapasitas", "Lokasi", "NamaRuangan" },
                values: new object[,]
                {
                    { 1, 12, "Lantai 1", "Ruang Rapat Merah" },
                    { 2, 150, "Lantai 2", "Aula Serbaguna" },
                    { 3, 20, "Lantai 1", "Creative Hub" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ruangans");
        }
    }
}
