using FantasticSpaceVoyage.Component;
using Godot;

namespace FantasticSpaceVoyage.Player;

public partial class Player : CharacterBody2D
{
    [Export] private VelocityComponent _velocityComponent;

    private AnimationTree _animationTree;
    private Node2D _visuals;

    public override void _Ready()
    {
        _velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
        
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _animationTree.Active = true;
        
        _visuals = GetNode<Node2D>("Visuals");
    }

    public override void _Process(double delta)
    {
        var isAttacking = Input.IsActionPressed("PlayerAttack");
        _animationTree.Set("parameters/conditions/IsAttacking", isAttacking);
        if (isAttacking) return;
        
        var direction = Input.GetVector(
            "MoveLeft", "MoveRight", "MoveUp", "MoveDown"
        );
        _velocityComponent.AccelerateInDirection(direction);

        var isWalking = direction is not { X: 0, Y: 0 };
        _animationTree.Set("parameters/conditions/IsWalking", isWalking);
        _animationTree.Set("parameters/conditions/IsIdle", !isWalking);
        
        if (isWalking)
        {
            var sign = direction.Sign().X;
            if (sign != 0) _visuals.Scale = _visuals.Scale with { X = sign };
        }

        var isRolling = Input.IsActionPressed("DodgeRoll");
        _animationTree.Set("parameters/conditions/IsRolling", isRolling);
        
        _velocityComponent.Move(this);
    }
}