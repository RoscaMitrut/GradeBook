using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IGradeService
    {
        Task<List<Grade>> GetAllAsync();
        Task<List<Grade>> GetValidTopNAsync(int n);
        Task<Grade?> GetByIdAsync(int id);
    }
}