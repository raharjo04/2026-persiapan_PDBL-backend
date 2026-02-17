using System.ComponentModel.DataAnnotations;
namespace BlazorApp1.DTOs;

public record RuanganRequestDTO(
    [Required(ErrorMessage = "Nama ruangan wajib diisi")]
    string NamaRuangan, 
    [Required(ErrorMessage = "Kapasitas wajib diisi")]
    int Kapasitas, 
    [Required(ErrorMessage = "Lokasi wajib diisi")]
    string Lokasi
);

public record RuanganResponseDTO(
    int Id, 
    string NamaRuangan, 
    int Kapasitas, 
    string Lokasi
);