using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;
    private readonly ILogger<GradeController> _logger;
    private readonly IGradeStatisticsService _gradeStatisticsService;

    public GradeController(IGradeService gradeService, IGradeStatisticsService statisticsService, ILogger<GradeController> logger)
    {
        _gradeService = gradeService;
        _gradeStatisticsService = statisticsService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade called");

        var grades = await _gradeService.GetAllAsync();

        var statistics = await _gradeStatisticsService.ComputeStatisticsAsync();

        _logger.LogInformation($"[LOG] Returning statistics: {statistics}");

        return Ok(new
        {
            Data = grades,
            Statistics = statistics
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade/{id} called");

        if (id <= 0)
        {
            _logger.LogInformation($"[LOG] Invalid id: {id}");
            return BadRequest("Id must be a positive integer.");
        }

        var grade = await _gradeService.GetByIdAsync(id);

        if (grade == null)
        {
            _logger.LogInformation($"[LOG] Grade {id} not found");
            return NotFound($"Grade with Id {id} was not found.");
        }

        return Ok(grade);
    }

    [HttpGet("top/{n}")]
    public async Task<IActionResult> GetTopN(int n)
    {
        _logger.LogInformation($"[LOG] {DateTime.UtcNow}: GET api/grade/top/{n} called");
        if (n <= 0)
        {
            _logger.LogInformation($"[LOG] Invalid n: {n}");
            return BadRequest("N must be a positive integer.");
        }
        var topGrades = await _gradeService.GetValidTopNAsync(n);
        return Ok(topGrades);
    }
}