using API_DndBot.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<DndBotContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseCors("CorsPolicy");

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();