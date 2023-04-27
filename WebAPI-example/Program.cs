using Microsoft.Net.Http.Headers;
using WebAPI_example.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddSingleton<IRepository, Repository>();
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

app.UseCors(builder =>
{
    builder
    // create rules
    .WithOrigins("http://localhost:5281")
    //.WithHeaders(HeaderNames.ContentType, "x-customer-header")
    //.WithOrigins(new string[] {"http://localhost:5281", "domain.com", "*.domain2.com"})
    //.WithHeaders("h1", "h2")
    //.WithMethods("GET")

    // allow anything
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin();
});

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
