using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;
using System.Text.Json;

namespace Siemens.Internship2026.GradeBook.Repositories
{
    public class GradeExternalInitMemoryRepository : GradeMemoryRepository, IGradeReader, IGradeWriter
    {
        
        private const string DataInitURL = "https://gist.githubusercontent.com/ArdeleanTudor/8ea407832cd9794960e0e6bbd1319f6e/raw";
        private static readonly HttpClient _httpClient = new();

        private readonly ILogger<GradeExternalInitMemoryRepository> _logger;

        public GradeExternalInitMemoryRepository(ILogger<GradeExternalInitMemoryRepository> logger)
        {
            _logger = logger;
            InitializeDataAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(DataInitURL);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var doc = System.Text.Json.JsonDocument.Parse(content);
                var grades = doc.RootElement.GetProperty("items")
                                .Deserialize<List<Grade>>(options);

                if (grades != null)
                {
                    foreach (var grade in grades)
                    {
                        grade.Id = _nextId++;
                        _grades.Add(grade);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize grades from external source.");
            }
        }
    }
}
