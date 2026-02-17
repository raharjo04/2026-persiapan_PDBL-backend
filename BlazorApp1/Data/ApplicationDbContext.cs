using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models;

namespace BlazorApp1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Ruangan> Ruangans { get; set; }
    public DbSet<Peminjaman> Peminjamans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ruangan>().HasData(
            new Ruangan { Id = 1, NamaRuangan = "Ruang Rapat Merah", Kapasitas = 12, Lokasi = "Lantai 1" },
            new Ruangan { Id = 2, NamaRuangan = "Aula Serbaguna", Kapasitas = 150, Lokasi = "Lantai 2" },
            new Ruangan { Id = 3, NamaRuangan = "Creative Hub", Kapasitas = 20, Lokasi = "Lantai 1" }
        );

        modelBuilder.Entity<Peminjaman>().HasData(
            new Peminjaman 
            { 
                Id = 1, 
                RuanganId = 1, 
                NamaPeminjam = "Diaz", 
                TanggalPinjam = DateTime.SpecifyKind(new DateTime(2026, 2, 12), DateTimeKind.Utc), 
                Keperluan = "Koordinasi Proyek PBL", 
                Status = "Menunggu"
            },
            new Peminjaman 
            { 
                Id = 2, 
                RuanganId = 2, 
                NamaPeminjam = "Rina", 
                TanggalPinjam = DateTime.SpecifyKind(new DateTime(2026, 2, 15), DateTimeKind.Utc), 
                Keperluan = "Seminar Teknologi Industri", 
                Status = "Disetujui" 
            }
        );
    }
}