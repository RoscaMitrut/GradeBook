using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IGradeWriter
    {
        Task<Grade> AddAsync(Grade grade);
        Task<Grade?> UpdateAsync(int id, Grade newGrade);
        Task<bool> DeleteAsync(int id);
    }
}
