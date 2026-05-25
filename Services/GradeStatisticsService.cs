using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Services
{
    public class GradeStatisticsService : IGradeStatisticsService
    {
        private readonly IGradeReader _gradeReader;

        public GradeStatisticsService(IGradeReader gradeReader)
        {
            _gradeReader = gradeReader;
        }

        public async Task<GradeStatisticsResult> ComputeStatisticsAsync()
        {
            var grades = await _gradeReader.GetAllAsync();

            return new GradeStatisticsResult(
                TotalCount: grades.Count(),
                AverageValue: grades.Any() ? grades.Average(i => i.Value) : 0,
                RetrievedAt: DateTime.UtcNow
            );

        }
    }
}
