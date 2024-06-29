using System;
using Godot;

namespace FantasticSpaceVoyage.GameObject.GameCamera;

public partial class GameCamera : Camera2D
{
    [Export] public int Smoothing = 20;

    public override void _Ready()
    {
        MakeCurrent();
    }

    public override void _Process(double delta)
    {
        if (GetTree().GetFirstNodeInGroup("Player") is Node2D player)
        {
            GlobalPosition = GlobalPosition.Lerp(player.GlobalPosition, (float)(1f - Math.Exp(-delta * Smoothing)));
        }
    }
}