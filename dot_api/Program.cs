
using dot_api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.MapGamesEndPoints();


app.Run();
