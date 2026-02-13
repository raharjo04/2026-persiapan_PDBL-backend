using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data;
using BlazorApp1.Models;
using BlazorApp1.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class RuanganController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public RuanganController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RuanganResponseDTO>>> GetRuangans()
    {
        var data = await _context.Ruangans
            .Where(r => !r.IsDeleted) 
            .Select(r => new RuanganResponseDTO(r.Id, r.NamaRuangan, r.Kapasitas, r.Lokasi))
            .ToListAsync();
            
        return Ok(data);
    }


    [HttpGet("{id}", Name = "GetRuangan")] 
    public async Task<ActionResult<RuanganResponseDTO>> GetRuangan(int id)
    {
        var ruangan = await _context.Ruangans.FindAsync(id);

        if (ruangan == null || ruangan.IsDeleted) return NotFound();

        return Ok(new RuanganResponseDTO(ruangan.Id, ruangan.NamaRuangan, ruangan.Kapasitas, ruangan.Lokasi));
    }

    [HttpPost]
    public async Task<ActionResult<RuanganResponseDTO>> PostRuangan(RuanganRequestDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (request.Kapasitas <= 0) return BadRequest("Kapasitas ruangan harus lebih dari 0.");

        var ruangan = new Ruangan {
            NamaRuangan = request.NamaRuangan,
            Kapasitas = request.Kapasitas,
            Lokasi = request.Lokasi
        };

        _context.Ruangans.Add(ruangan);
        await _context.SaveChangesAsync();

        var response = new RuanganResponseDTO(ruangan.Id, ruangan.NamaRuangan, ruangan.Kapasitas, ruangan.Lokasi);
        
        return CreatedAtAction("GetRuangan", new { id = ruangan.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRuangan(int id, RuanganRequestDTO request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (request.Kapasitas <= 0) return BadRequest("Kapasitas ruangan harus lebih dari 0.");

        var ruangan = await _context.Ruangans.FindAsync(id);
        
        if (ruangan == null || ruangan.IsDeleted) return NotFound();

        ruangan.NamaRuangan = request.NamaRuangan;
        ruangan.Kapasitas = request.Kapasitas;
        ruangan.Lokasi = request.Lokasi;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!RuanganExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRuangan(int id)
    {
        var ruangan = await _context.Ruangans.FindAsync(id);
        
        if (ruangan == null || ruangan.IsDeleted) return NotFound();

        ruangan.IsDeleted = true; 
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RuanganExists(int id) => _context.Ruangans.Any(e => e.Id == id && !e.IsDeleted);
}