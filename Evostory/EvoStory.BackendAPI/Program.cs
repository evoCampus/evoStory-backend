using Microsoft.EntityFrameworkCore;
using EvoStory.BackendAPI.Data;
using EvoStory.BackendAPI.Repository;
using EvoStory.BackendAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseInMemoryDatabase("StoryDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IChoiceRepository, ChoiceRepositoryInMemory>();
builder.Services.AddSingleton<IChoiceService, ChoiceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
