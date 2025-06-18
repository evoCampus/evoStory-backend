using EvoStory.BackendAPI.Data;
using EvoStory.BackendAPI.Repository;
using EvoStory.BackendAPI.Services;
using EvoStory.BackendAPI.Database;
using EvoStory.BackendAPI.Importer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiContext>(options =>
options.UseInMemoryDatabase("StoryDb"));

builder.Services.AddLogging(builder_ => builder_.AddConsole());

builder.Services.AddControllers();
builder.Services.AddSingleton<ISceneRepository, SceneRepositoryInMemory>();
builder.Services.AddSingleton<ISceneService, SceneService>();

builder.Services.AddSingleton<IChoiceRepository, ChoiceRepositoryInMemory>();
builder.Services.AddSingleton<IChoiceService, ChoiceService>();

builder.Services.AddSingleton<IStoryRepository, StoryRepositoryInMemory>();
builder.Services.AddSingleton<IStoryService, StoryService>();

builder.Services.AddSingleton<IDTOConversionService, DTOConversionService>();

builder.Services.AddSingleton<IDatabase, DatabaseInMemory>();

builder.Services.AddSingleton<IDefaultStoryImporter, DefaultStoryImporter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedSpecificOrigins = "allowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();
IDefaultStoryImporter defaultStoryImporter = app.Services.GetRequiredService<IDefaultStoryImporter>();
defaultStoryImporter.ImportStory();
app.UseCors(allowedSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
