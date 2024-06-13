using dot_api.Dtos;

namespace dot_api.EndPoints;

public static class GamesEndpoints
{
   const string GetGameEndpointName = "GetGame";

private static readonly List<GameDto> games = [
new(1,"Spiderman","Fiction",19.22M , new DateOnly(1992, 7, 15)),
new (2,"Final Fantasy","Roleplaying",5.99M,new DateOnly(2010, 9, 30)),
new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
];


public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
{
    
var group = app.MapGroup("games");


//handling is the arrong function 
group.MapGet("/", () => games);

// GET  / games/
group.MapGet("/{id}",(int id) =>  {

  GameDto? game =  games.Find(game => game.Id == id);
  return game is null ? Results.NotFound() : Results.Ok(game);
  
    
    } ).WithName(GetGameEndpointName);


// POST /games
group.MapPost("/", (CreateGameDto newGame) => {


    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
}).WithParameterValidation();


// PUT / GAMES
group.MapPut("/{id}", (int id, UpdateGameDto updateGame) => {
   var index  = games.FindIndex(game => game.Id == id);

    if(index == -1){
      return Results.NotFound();
    }

   games[index] = new GameDto(
    id,
    updateGame.Name,
    updateGame.Genre,
    updateGame.Price,
    updateGame.ReleaseDate
   );

   return Results.NoContent();
});


// DELETE / games/1
app.MapDelete("/{id}", (int id) => 
{
    games.RemoveAll(game => game.Id == id);  
    return Results.NoContent();  

} );

return group;

}




}
