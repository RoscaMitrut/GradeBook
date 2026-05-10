using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services;

public class GradeService : IGradeService
{
    private readonly IGradeReader _gradeReader;
    
    public GradeService(IGradeReader gradeReader)
    {
        _gradeReader = gradeReader;
    }

    public async Task<List<Grade>> GetAllAsync()
    {
        var grades = await _gradeReader.GetAllAsync();
        return grades.ToList();
    }

    public async Task<Grade?> GetByIdAsync(int id)
    {
        return await _gradeReader.GetByIdAsync(id);
    }

    public async Task<List<Grade>> GetValidTopNAsync(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentException("N must be greater than 0");
        }
        
        var grades = await _gradeReader.GetAllAsync();

        return grades.Where(grade => grade.IsValid())
                    .OrderByDescending(grade => grade.Value)
                    .Take(n)
                    .ToList();
    }
}
