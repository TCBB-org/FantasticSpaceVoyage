using DSurvivorsCourse.Scenes.Component;
using Godot;

namespace FantasticSpaceVoyage.Player;

public partial class Player : CharacterBody2D
{
    [Export] private VelocityComponent _velocityComponent;

    public override void _Ready()
    {
        _velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
    }

    public override void _Process(double delta)
    {
        var direction = Input.GetVector(
            "MoveLeft", "MoveRight", "MoveUp", "MoveDown"
        );
        _velocityComponent.AccelerateInDirection(direction);
        _velocityComponent.Move(this);
    }
}