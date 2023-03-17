using Prometheus;

namespace Mars.MissionControl;

public record Player
{
    public Player(string name)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        Name = name;
        Token = PlayerToken.Generate();
        PerseveranceLocation = new Location(0, 0);
        IngenuityLocation = new Location(0, 0);
        var labels = new string[] { $"token_{Token.Value}"};
        SuccessfulRoverMoveCounter = Metrics.CreateCounter("successful_rover_move_total", "Number of times a rover has successfully moved per token" , labelNames: labels);
        SuccessfulHelicopterMoveCounter = Metrics.CreateCounter("successful_heli_move_total", "Number of times a heli has successfully moved per token", labelNames: labels);
        SquaresMovedByHeliCopterCounter = Metrics.CreateCounter("squares_moved_by_helicopter_total", "Number of squares a helicopter has moved", labelNames: labels);
        RoverDamageCounter = Metrics.CreateCounter("rover_damage_total", "Total amount of damage a rover takes", labelNames: labels);
    }
    public Counter SuccessfulRoverMoveCounter;

    public Counter SuccessfulHelicopterMoveCounter;
    public Counter SquaresMovedByHeliCopterCounter;
    public Counter RoverDamageCounter;

    public TimeSpan? WinningTime { get; set; }
    public long BatteryLevel { get; init; }
    public PlayerToken Token { get; private set; }
    public string Name { get; private set; }
    public Location PerseveranceLocation { get; init; }
    public Location IngenuityLocation { get; init; }
    public int IngenuityBatteryLevel { get; init; }
    public Orientation Orientation { get; init; }
}
