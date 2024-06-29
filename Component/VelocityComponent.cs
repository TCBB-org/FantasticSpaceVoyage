using System;
using Godot;

namespace FantasticSpaceVoyage.Component;

public partial class VelocityComponent : Node
{
    [Export] private uint _maxSpeed = 70;
    [Export] private double _acceleration = 25;

    public Vector2 Velocity { get; private set; } = Vector2.Zero;

    public Vector2 AccelerateToPlayer()
    {
        if (Owner is not Node2D owner) return Vector2.Zero;
        if (GetTree().GetFirstNodeInGroup("Player") is not Node2D player) return Vector2.Zero;

        var directionToPlayer = (player.GlobalPosition - owner.GlobalPosition).Normalized();
        AccelerateInDirection(directionToPlayer);
        
        return directionToPlayer;
    }

    public void AccelerateInDirection(Vector2 direction)
    {
        var desiredVelocity = direction * _maxSpeed;
        Velocity = Velocity.Lerp(desiredVelocity, (float)(1 - Math.Exp(-_acceleration * GetProcessDeltaTime())));
    }

    public void Decelerate() => AccelerateInDirection(Vector2.Zero);

    public void Move(CharacterBody2D body)
    {
        body.Velocity = Velocity;
        body.MoveAndSlide();
        Velocity = body.Velocity; // in case we collide with something
    }
}