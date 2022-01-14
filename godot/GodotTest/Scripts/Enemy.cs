using Godot;
using System;

public class Enemy : KinematicBody2D
{
	private const float SPEED = 600f;
	private const float GRAVITY = 20f;
	private const float MIN_POSITION_X = -1550f;
	private const float MAX_POSITION_X = 3150f;
	private float directionX = SPEED;
	private Vector2 enemy;


	public override void _PhysicsProcess(float delta)
	{
		EnemyMovement();
	}

	private void EnemyMovement()
	{
		if (isEnemyCrossMinPositionX()) 
		{
			directionX = SPEED;
			GetNode<AnimatedSprite>("Animation").FlipH = false;
		} else if (isEnemyCrossMaxPositionX())
		{
			directionX = -SPEED;
			GetNode<AnimatedSprite>("Animation").FlipH = true;
		}

		enemy.x = directionX;
		enemy.y += GRAVITY;
		enemy = MoveAndSlide(enemy);
	}

	private bool isEnemyCrossMinPositionX()
	{
		return Position.x < MIN_POSITION_X;
	}

	private bool isEnemyCrossMaxPositionX()
	{
		return Position.x > MAX_POSITION_X;
	}
}
