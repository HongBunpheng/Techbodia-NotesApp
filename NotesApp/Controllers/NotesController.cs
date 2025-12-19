using Microsoft.AspNetCore.Mvc;
using NotesApp.Repositories;

namespace NotesApp.Controllers;
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly NoteRepository _repo;
    private const int MockUserId = 1;

    public NotesController(NoteRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notes = await _repo.GetAllAsync(MockUserId);
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var note = await _repo.GetByIdAsync(id, MockUserId);
        if (note == null) return NotFound();
        return Ok(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title is required");

        await _repo.CreateAsync(dto, MockUserId);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateNoteDto dto)
    {
        await _repo.UpdateAsync(id, dto, MockUserId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id, MockUserId);
        return Ok();
    }
}
