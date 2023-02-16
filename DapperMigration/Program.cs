using DapperMigration.BackgroundService;
using DapperMigration.Persistence;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var axisConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<MySqlConnection>(_ => 
    new MySqlConnection(axisConnectionString));
builder.Services.AddTransient<MigrationService>(_ => 
    new MigrationService(axisConnectionString));
builder.Services.AddHostedService<MigrationRunner>();

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
