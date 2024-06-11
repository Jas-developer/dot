using dot_api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
new(1,"Spiderman","Fiction",19.22M , new DateOnly(1992, 7, 15)),
new (2,"Final Fantasy","Roleplaying",5.99M,new DateOnly(2010, 9, 30)),
new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 9, 27))
];

//List of users
List<UsersDto> users = [
new(1, "DevJas", 20),
new(2,"James Larino",21),
new(3, "John Suello", 23)
];

//my first api in DOTNET
//handling is the arrong function 
app.MapGet("games", () => games);

// GET  / games/
app.MapGet("games/{id}",(int id) => games.Find(game => game.Id == id) ).WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id}, game);
});

// PUT / GAMES
app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) => {
   var index  = games.FindIndex(game => game.Id == id);
   games[index] = new GameDto(
    id,
    updateGame.Name,
    updateGame.Genre,
    updateGame.Price,
    updateGame.ReleaseDate
   );

   return Results.NoContent();
});





app.Run();
