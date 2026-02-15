using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data;
using BlazorApp1.Models;
using BlazorApp1.DTOs;

namespace BlazorApp1.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class PeminjamanController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PeminjamanController(ApplicationDbContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult> Create(PeminjamanRequestDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var ruanganExists = await _context.Ruangans.AnyAsync(r => r.Id == request.RuanganId);
        if (!ruanganExists) return BadRequest("Ruangan tidak ditemukan.");

        var peminjaman = new Peminjaman {
            RuanganId = request.RuanganId,
            NamaPeminjam = request.NamaPeminjam,
            TanggalPinjam = DateTime.SpecifyKind(request.TanggalPinjam, DateTimeKind.Utc),
            Keperluan = request.Keperluan,
            Status = "Menunggu"
        };

        _context.Peminjamans.Add(peminjaman);
        await _context.SaveChangesAsync();
        
        return Ok("Peminjaman berhasil diajukan dengan status: Menunggu");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PeminjamanResponseDTO>>> GetAll([FromQuery] string? status)
    {
        var query = _context.Peminjamans.Include(p => p.Ruangan).AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(p => p.Status == status);
        }

        var data = await query
            .Select(p => new PeminjamanResponseDTO(
                p.Id, 
                p.RuanganId, // <--- TAMBAHAN: Kirim ID supaya dropdown di Frontend terisi
                p.Ruangan!.NamaRuangan, 
                p.NamaPeminjam, 
                p.TanggalPinjam, 
                p.Keperluan, 
                p.Status))
            .ToListAsync();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PeminjamanResponseDTO>> GetById(int id)
    {
        var p = await _context.Peminjamans
            .Include(p => p.Ruangan)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (p == null) return NotFound();

        return Ok(new PeminjamanResponseDTO(
            p.Id, 
            p.RuanganId, // <--- TAMBAHAN
            p.Ruangan!.NamaRuangan, 
            p.NamaPeminjam, 
            p.TanggalPinjam, 
            p.Keperluan, 
            p.Status));
    }

    // ==========================================
    // TAMBAHAN BARU: Method PUT untuk Update Data
    // ==========================================
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PeminjamanRequestDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound("Data peminjaman tidak ditemukan.");

        // Validasi apakah ruangan ada
        var ruanganExists = await _context.Ruangans.AnyAsync(r => r.Id == request.RuanganId);
        if (!ruanganExists) return BadRequest("Ruangan tidak ditemukan.");

        // Update Field
        peminjaman.RuanganId = request.RuanganId;
        peminjaman.NamaPeminjam = request.NamaPeminjam;
        peminjaman.TanggalPinjam = DateTime.SpecifyKind(request.TanggalPinjam, DateTimeKind.Utc);
        peminjaman.Keperluan = request.Keperluan;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!_context.Peminjamans.Any(e => e.Id == id)) return NotFound();
            else throw;
        }

        return Ok("Data peminjaman berhasil diperbarui.");
    }

    [HttpPatch("{id}/status")] 
    public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound();

        var validStatus = new[] { "Menunggu", "Disetujui", "Ditolak" };
        if (!validStatus.Contains(request.Status)) 
            return BadRequest("Status tidak valid. Gunakan: Menunggu, Disetujui, atau Ditolak.");

        peminjaman.Status = request.Status;
        await _context.SaveChangesAsync();
        
        return Ok($"Status peminjaman ID {id} diubah menjadi {request.Status}");
    }

    [HttpDelete("{id}")] 
    public async Task<IActionResult> Delete(int id)
    {
        var peminjaman = await _context.Peminjamans.FindAsync(id);
        if (peminjaman == null) return NotFound();

        _context.Peminjamans.Remove(peminjaman);
        await _context.SaveChangesAsync();
        
        return NoContent(); 
    }
}