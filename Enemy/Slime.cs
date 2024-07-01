using FantasticSpaceVoyage.Player;
using Godot;
using System;

public partial class Slime : CharacterBody2D
{
	[Export] private float _minJumpCooldown;
	[Export] private float _jumpForce;

	private float _jumpCooldown;
	private Player _player;
	private AnimationTree _animationTree;

	public override void _Ready()
	{
		_player = GetNode<Player>("../Player");
		_animationTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _PhysicsProcess(double delta)
	{
		_jumpCooldown -= (float)delta;
		_animationTree.Set("parameters/conditions/Jump", false);
		if (_jumpCooldown <= 0)
		{
			Velocity += Position.DirectionTo(_player.Position) * _jumpForce;
			_jumpCooldown = _minJumpCooldown + Random.Shared.NextSingle() * 0.2f;
			_animationTree.Set("parameters/conditions/Jump", true);
		}

		Velocity = Velocity.Lerp(Vector2.Zero, 0.1f);
		MoveAndSlide();
	}
}
