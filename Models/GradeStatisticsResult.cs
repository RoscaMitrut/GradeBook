namespace Siemens.Internship2026.GradeBook.Models;

public sealed record GradeStatisticsResult(
    int TotalCount,
    decimal AverageValue,
    DateTime RetrievedAt
);