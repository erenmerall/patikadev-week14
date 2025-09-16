using Microsoft.EntityFrameworkCore;
using IdentityAndDataProtection.Data;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller desteği
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // root URL'yi swagger'a yönlendirmek için kullnanılır
    app.MapGet("/", () => Results.Redirect("/swagger"));
}


app.UseHttpsRedirection();
app.MapControllers();

app.Run();
