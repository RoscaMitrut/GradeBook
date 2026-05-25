using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;
using Siemens.Internship2026.GradeBook.Services;

var builder = WebApplication.CreateBuilder(args);
var api_key = "asdfasdf12341234asdfasdf";
builder.Services.AddControllers();

builder.Services.AddSingleton<GradeExternalInitMemoryRepository>();
builder.Services.AddSingleton<IGradeReader>(x => x.GetRequiredService<GradeExternalInitMemoryRepository>());
builder.Services.AddSingleton<IGradeWriter>(x => x.GetRequiredService<GradeExternalInitMemoryRepository>());

builder.Services.AddScoped<IGradeStatisticsService, GradeStatisticsService>();
builder.Services.AddScoped<IGradeService, GradeService>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
