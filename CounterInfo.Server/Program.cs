
//#region Setting
//using CounterInfo.Server;
//using CounterInfo.Server.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();
//#endregion



//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");

//app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}


using CounterInfoServer.Models;
using Microsoft.EntityFrameworkCore;
using CounterInfo.Server.Endpoints;
using CounterInfo.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseSqlite("Data Source=sample.db"));
//builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



//using (var scope = app.Services.CreateScope())
//{
//    var options = new DbContextOptionsBuilder<TodoDb>()
//    .UseSqlite("Data Source=sample.db")
//    .Options;

//    using var db = new TodoDb(options);
//    //var db = scope.ServiceProvider.GetRequiredService<TodoDb>();
//    db.Database.EnsureCreated();
//}


var options = new DbContextOptionsBuilder<TodoDb>()
.UseSqlite("Data Source=sample.db")
.Options;

using var db = new TodoDb(options);
db.Database.EnsureCreated();

#region Registering endpoint
app.RegisterCounterInfomationEndPoint();
app.RegisterTodoItemEndPoint();
#endregion

app.Run();