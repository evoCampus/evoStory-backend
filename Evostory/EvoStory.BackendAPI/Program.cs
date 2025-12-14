using EvoStory.Database;
using EvoStory.BackendAPI.Services;
using EvoStory.BackendAPI.Importer;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDatabaseServices(connectionString!);

//Iservice

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//  options.UseSqlServer(connectionString));

// Add services to the container.


builder.Services.AddLogging(builder_ => builder_.AddConsole());

builder.Services.AddControllers();

builder.Services.AddScoped<ISceneService, SceneService>();

builder.Services.AddScoped<IChoiceService, ChoiceService>();

builder.Services.AddScoped<IStoryService, StoryService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDTOConversionService, DTOConversionService>();

//builder.Services.AddScoped<IDatabase, DatabaseInMemory>();

builder.Services.AddScoped<IStoryImporter, DefaultStoryImporter>();

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
using (var scope = app.Services.CreateScope())
{
	var scopedServices = scope.ServiceProvider;
	var defaultStoryImporter = scopedServices.GetRequiredService<IStoryImporter>();
	defaultStoryImporter.ImportStory();
}
app.UseCors(allowedSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();

app.MapControllers();


app.Run();
