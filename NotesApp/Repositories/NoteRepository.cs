using Dapper;
using Microsoft.Data.SqlClient;
namespace NotesApp.Repositories;

public class NoteRepository
{
    private readonly IConfiguration _config;

    public NoteRepository(IConfiguration config)
    {
        _config = config;
    }

    private SqlConnection GetConnection()
        => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

    public async Task<IEnumerable<Note>> GetAllAsync(int userId)
    {
        var sql = "SELECT * FROM Notes WHERE UserId = @UserId ORDER BY CreatedAt DESC";
        using var db = GetConnection();
        return await db.QueryAsync<Note>(sql, new { UserId = userId });
    }

    public async Task<Note?> GetByIdAsync(int id, int userId)
    {
        var sql = "SELECT * FROM Notes WHERE Id = @Id AND UserId = @UserId";
        using var db = GetConnection();
        return await db.QueryFirstOrDefaultAsync<Note>(sql, new { Id = id, UserId = userId });
    }

    public async Task CreateAsync(CreateNoteDto dto, int userId)
    {
        var sql = @"
            INSERT INTO Notes (Title, Content, CreatedAt, UserId)
            VALUES (@Title, @Content, GETDATE(), @UserId)";
        using var db = GetConnection();
        await db.ExecuteAsync(sql, new { dto.Title, dto.Content, UserId = userId });
    }

    public async Task UpdateAsync(int id, UpdateNoteDto dto, int userId)
    {
        var sql = @"
            UPDATE Notes
            SET Title = @Title,
                Content = @Content,
                UpdatedAt = GETDATE()
            WHERE Id = @Id AND UserId = @UserId";
        using var db = GetConnection();
        await db.ExecuteAsync(sql, new { dto.Title, dto.Content, Id = id, UserId = userId });
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var sql = "DELETE FROM Notes WHERE Id = @Id AND UserId = @UserId";
        using var db = GetConnection();
        await db.ExecuteAsync(sql, new { Id = id, UserId = userId });
    }
}
