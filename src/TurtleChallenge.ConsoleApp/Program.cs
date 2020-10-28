using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TurtleChallenge.ConsoleApp.Settings;
using TurtleChallenge.Core;
using TurtleChallenge.Core.Domain;

namespace TurtleChallenge.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please pass 2 arguments: GAME-SETTINGS file name and MOVES files name");
                Environment.Exit(0);
            }

            var gameSettingsFileName = args[0];
            var movesFileName = args[1];
            var gameSettings = await GetAsync<GameSettings>(gameSettingsFileName);
            var moveSettings = await GetAsync<MovesSettings>(movesFileName);
            var game = Bootstrap(gameSettings);
            var sequences = moveSettings.Sequences.Select(s => s.Select(Enum.Parse<Moves>).ToList()).ToList();

            for (var i = 0; i < sequences.Count; i++)
            {
                var result = game.Play(sequences[i]);
                Console.WriteLine($"Sequence {i + 1}: {result.Message}");

            }
        }

        private static Game Bootstrap(GameSettings settings)
        {
            var services = new ServiceCollection();

            services.AddSingleton(sp =>
            {
                var board = settings.Board;
                var mines = board.Mines.Select(p => new Point(p.X, p.Y)).ToHashSet();
                var exit = new Point(board.ExitPoint.X, board.ExitPoint.Y);
                return new Board(board.Size.Rows, board.Size.Columns, mines, exit);
            });

            services.AddSingleton(sp =>
            {
                var turtle = settings.Turtle;
                var direction = Enum.Parse<Directions>(turtle.Direction);
                var position = new Point(turtle.StartingPosition.X, turtle.StartingPosition.Y);
                return new Turtle(direction, position);
            });

            services.AddSingleton<Game>();

            return services.BuildServiceProvider().GetService<Game>();
        }

        private static async Task<T> GetAsync<T>(string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, $"{fileName}.json");
            if (File.Exists(path))
            {
                var content = await File.ReadAllTextAsync(path);
                return JsonConvert.DeserializeObject<T>(content);
            }
         
            throw new ArgumentException($"{fileName} NOT FOUND at {path}.");
        }
    }
}
