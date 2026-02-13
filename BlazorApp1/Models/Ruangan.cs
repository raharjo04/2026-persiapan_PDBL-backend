using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public class Ruangan
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama ruangan wajib diisi")] // Validasi: Tidak boleh kosong
    [StringLength(100)]
    public string NamaRuangan { get; set; } = string.Empty;

    [Required]
    [Range(1, 500, ErrorMessage = "Kapasitas harus antara 1-500")] // Validasi: Angka harus positif
    public int Kapasitas { get; set; }

    [Required]
    public string Lokasi { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}