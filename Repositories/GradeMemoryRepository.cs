using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class GradeMemoryRepository : IGradeReader, IGradeWriter
{
    protected readonly List<Grade> _grades = new();
    protected int _nextId = 1;

    public Task<Grade?> GetByIdAsync(int id)
    {
        var grade = _grades.FirstOrDefault(i => i.Id == id && i.IsActive);
        return Task.FromResult(grade);
    }

    public Task<IEnumerable<Grade>> GetAllAsync()
    {
        var grades = _grades.Where(i => i.IsActive).AsEnumerable();
        return Task.FromResult(grades);
    }

    public Task<Grade> AddAsync(Grade grade)
    {
        grade.Id = _nextId++;
        _grades.Add(grade);
        return Task.FromResult(grade);
    }

    public Task<Grade?> UpdateAsync(int id, Grade newGrade)
    {
        var index = _grades.FindIndex(i => i.Id == id);
        if (index == -1) return Task.FromResult<Grade?>(null);
        newGrade.Id = id;
        _grades[index] = newGrade;
        return Task.FromResult<Grade?>(newGrade);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var grade = _grades.FirstOrDefault(i => i.Id == id);
        if (grade == null) return Task.FromResult(false);
        grade.IsActive = false;
        return Task.FromResult(true);
    }
}
