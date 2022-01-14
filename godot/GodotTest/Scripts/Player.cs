using Godot;
using System;

public class Player : KinematicBody2D
{
	private const float SPEED = 400f;
	private const float GRAVITY = 20f;
	private const float JUMP_FORCE = -900f;
	private Vector2 player = Vector2.Zero;
	private Vector2 upDirection = Vector2.Up;
	private AnimatedSprite animation;

	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite>("PlayerAnimation");
	}

	public override void _PhysicsProcess(float delta)
	{
		PlayerMovement();
	}

	private void PlayerMovement() 
	{
		if (Input.IsActionPressed("move_right"))
		{
			player.x = SPEED;
			AnimateMovement(isMove: true, isMoveRight: true);
		} else if (Input.IsActionPressed("move_left"))
		{
			player.x = -SPEED;
			AnimateMovement(isMove: true, isMoveRight: false);
		} else
		{
			player.x = 0f;
			AnimateMovement(false, false);
		}

		player.y += GRAVITY;
		if (IsOnFloor() && Input.IsActionJustPressed("jump"))
			player.y = JUMP_FORCE;

		player = MoveAndSlide(player, upDirection);
	}

	void AnimateMovement(bool isMove, bool isMoveRight)
	{
		if (!isMove)
		{
			animation.Play("Idle");
			return;
		} 
		animation.Play("Flap");

		if (isMoveRight)
			animation.FlipH = false;
		else 
			animation.FlipH = true;
	}
	
	/**
	 * reciever method from signal body_entered() 
	 * connected to node: Area2D type: Area2D
	 * parent node: Player
	 */
	private void _OnPlayerBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Enemy"))
		{
			GetParent()
				.GetNode<MainCamera>("MainCamera")
				.SetPlayerDied(isPlayerDied: true);
			GetParent<Gameplay>().PlayerDied();
			QueueFree();
		}
	}
}
