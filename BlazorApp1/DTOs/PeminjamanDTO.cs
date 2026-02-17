using System.ComponentModel.DataAnnotations;
namespace BlazorApp1.DTOs;

public record PeminjamanRequestDTO(
    [Required(ErrorMessage = "Ruangan harus dipilih")]
    int RuanganId, 
    [Required(ErrorMessage = "Nama peminjam wajib diisi")]
    string NamaPeminjam, 
    [Required(ErrorMessage = "Tanggal peminjaman wajib diisi")]
    DateTime TanggalPinjam, 
    [Required(ErrorMessage = "Keperluan wajib diisi")]
    string Keperluan
);

public record PeminjamanResponseDTO(
    int Id, 
    int RuanganId,
    string NamaRuangan, 
    string NamaPeminjam, 
    DateTime TanggalPinjam, 
    string Keperluan, 
    string Status
);

public record UpdateStatusDTO(string Status);