using EvoStory.BackendAPI.Data;
using EvoStory.BackendAPI.Repository;
using EvoStory.BackendAPI.Services;
using EvoStory.BackendAPI.Database;
using EvoStory.BackendAPI.Importer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApiContext>(options =>
options.UseInMemoryDatabase("StoryDb"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth.cookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.LoginPath = "/api/User/login";
        options.AccessDeniedPath = "/api/User/access-denied";

        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddLogging(builder_ => builder_.AddConsole());

builder.Services.AddControllers();
builder.Services.AddSingleton<ISceneRepository, SceneRepositoryInMemory>();
builder.Services.AddSingleton<ISceneService, SceneService>();

builder.Services.AddSingleton<IChoiceRepository, ChoiceRepositoryInMemory>();
builder.Services.AddSingleton<IChoiceService, ChoiceService>();

builder.Services.AddSingleton<IStoryRepository, StoryRepositoryInMemory>();
builder.Services.AddSingleton<IStoryService, StoryService>();

builder.Services.AddSingleton<IUserRepository, UserRepositoryInMemory>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddSingleton<IDTOConversionService, DTOConversionService>();

builder.Services.AddSingleton<IDatabase, DatabaseInMemory>();

builder.Services.AddSingleton<IStoryImporter, DefaultStoryImporter>();

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
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
});

var app = builder.Build();
IStoryImporter defaultStoryImporter = app.Services.GetRequiredService<IStoryImporter>();
defaultStoryImporter.ImportStory();
app.UseCors(allowedSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
