using EvoStory.BackendAPI.Importer;
using EvoStory.BackendAPI.Services;
using EvoStory.Database;
using EvoStory.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDatabaseServices(connectionString!);


builder.Services.AddLogging(builder_ => builder_.AddConsole());

builder.Services.AddControllers();

builder.Services.AddScoped<ISceneService, SceneService>();

builder.Services.AddScoped<IChoiceService, ChoiceService>();

builder.Services.AddScoped<IStoryService, StoryService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IDTOConversionService, DTOConversionService>();

builder.Services.AddScoped<IStoryImporter, DefaultStoryImporter>();

builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "EvoStorySession";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;


        //401
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });



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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
