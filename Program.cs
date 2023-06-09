using Microsoft.EntityFrameworkCore;
using CodeWarsBackend.Services;
using CodeWarsBackend.Services.Context;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<KataService>();


var connectionString = builder.Configuration.GetConnectionString("MyCodeWarsString");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options => {
    options.AddPolicy("CodeWarPolicy",
    builder => {
        builder.WithOrigins("https://localhost:7000", "https://localhost:3000", "https://codewarreservation.azurewebsites.net/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("CodeWarPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
