using FantasticSpaceVoyage.Component;
using Godot;

namespace FantasticSpaceVoyage.Player;

public partial class Player : CharacterBody2D
{
    [Export] private VelocityComponent _velocityComponent;
    
    private AnimationPlayer _animationPlayer;
    private Node2D _visuals;

    public override void _Ready()
    {
        _velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _visuals = GetNode<Node2D>("Visuals");
    }

    public override void _Process(double delta)
    {
        var direction = Input.GetVector(
            "MoveLeft", "MoveRight", "MoveUp", "MoveDown"
        );
        _velocityComponent.AccelerateInDirection(direction);
        
        if (direction is { X: 0, Y: 0 }) _animationPlayer.Play("Idle");
        else
        {
            var sign = direction.Sign().X;
            if (sign != 0) _visuals.Scale = _visuals.Scale with { X = sign };

            _animationPlayer.Play("Walk");
        }
        
        _velocityComponent.Move(this);
    }
}