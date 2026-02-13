using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public class Peminjaman
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ruangan harus dipilih")]
    public int RuanganId { get; set; }

    [Required(ErrorMessage = "Nama peminjam wajib diisi")]
    [StringLength(100, ErrorMessage = "Nama terlalu panjang")]
    public string NamaPeminjam { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tanggal peminjaman tidak boleh kosong")]
    public DateTime TanggalPinjam { get; set; }

    [Required]
    public string Keperluan { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = "Menunggu";

    public Ruangan? Ruangan { get; set; }
}