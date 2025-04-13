using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)
    ),
    new (
        2,
        "Final Fantasy XIX",
        "RPG",
        59.99M,
        new DateOnly(2010, 9, 30)
    ),
    new (
        3,
        "FIFA 23",
        "Sports",
        69.99M,
        new DateOnly(2022, 9, 27)
    ),
];

app.MapGet("/", () => "Hello World!");

app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);

app.MapPost("games", (CreateGameDto newGame) => {
   GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
   ); 

   games.Add(game);

   return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

app.MapPut("games/{id}", (int id, UpdateGameDto updateGame) => {
    var index = games.FindIndex(game => game.Id == id);

    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );

    return Results.NoContent();
});

app.MapDelete("games/{id}", (int id) => {
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
