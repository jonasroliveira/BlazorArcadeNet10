using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace SnakeGame.Pages;

public partial class Home : ComponentBase
{

    private const int GridWidth = 20;
    private const int GridHeight = 20;

    private List<Coordinate> snake = new();
    private Coordinate food = new Coordinate(0, 0);
    private Direction currentDirection = Direction.Right;
    private int score = 0;
    private bool gameOver = false;
    private PeriodicTimer? gameTimer;

    protected override async Task OnInitializedAsync()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        snake = new List<Coordinate> { new(10, 10), new(10, 11), new(10, 12) };
        SpawnFood();
        score = 0;
        gameOver = false;
        StartLoop();
    }

    private async void StartLoop()
    {
        gameTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(150));
        while (await gameTimer.WaitForNextTickAsync() && !gameOver)
        {
            MoveSnake();
            StateHasChanged(); // Força o Blazor a renderizar a tela
        }
    }

    private void MoveSnake()
    {
        var head = snake.First();
        var newHead = currentDirection switch
        {
            Direction.Up => new Coordinate(head.X, head.Y - 1),
            Direction.Down => new Coordinate(head.X, head.Y + 1),
            Direction.Left => new Coordinate(head.X - 1, head.Y),
            Direction.Right => new Coordinate(head.X + 1, head.Y),
            _ => head
        };

        if (newHead.X < 0 || newHead.X >= GridWidth || newHead.Y < 0 || newHead.Y >= GridHeight || snake.Any(s => s.X ==
        newHead.X && s.Y == newHead.Y))
        {
            gameOver = true;
            return;
        }

        snake.Insert(0, newHead);

        if (newHead.X == food.X && newHead.Y == food.Y)
        {
            score += 10;
            SpawnFood();
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }
    }

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        currentDirection = e.Key switch
        {
            "ArrowUp" when currentDirection != Direction.Down => Direction.Up,
            "ArrowDown" when currentDirection != Direction.Up => Direction.Down,
            "ArrowLeft" when currentDirection != Direction.Right => Direction.Left,
            "ArrowRight" when currentDirection != Direction.Left => Direction.Right,
            _ => currentDirection
        };
    }

    private void SpawnFood() => food = new Coordinate(Random.Shared.Next(0, GridWidth), Random.Shared.Next(0, GridHeight));

    private string GetCellClass(int x, int y)
    {
        if (snake.Any(s => s.X == x && s.Y == y)) return "snake-body";
        if (food.X == x && food.Y == y) return "food";
        return "";
    }

    public record Coordinate(int X, int Y);
    public enum Direction { Up, Down, Left, Right }
}