using Godot;
using System;

public class Gameplay : Node2D
{
	[Export]
	private PackedScene enemy;
	private Vector2 spawnLeft = new Vector2(x: -1400f, y: 500f);
	private Vector2 spawnRight = new Vector2(x: 3000, y: 500f);
	private Timer restart;
	public override void _Ready()
	{
		restart = GetNode<Timer>("Restart");
	}

	private void EnemySpawn()
	{
		Enemy newEnemy = enemy.Instance() as Enemy;
		newEnemy.Position = newEnemyGeneratePosition();
		AddChild(newEnemy);
	}

	private Vector2 newEnemyGeneratePosition()
	{
		if (isEnemySpawnOnRight())
		{
			return spawnRight;
		} else
		{
			return spawnLeft;
		}
	}

	private bool isEnemySpawnOnRight()
	{
		int random = new Random().Next(0, 2);
		return random > 0;
	}

	public void PlayerDied()
	{
		restart.Start();
	}

	/**
	 * reciever method from signal timeout() 
	 * connected to node: Timer type: Timer
	 * parent node: Gameplay
	 */
	private void _OnTimerTimeout()
	{
		EnemySpawn();
	}

	/**
	 * reciever method from signal timeout() 
	 * connected to node: Restart type: Timer
	 * parent node: Gameplay
	 */
	private void _OnPlayerDied()
	{
		GetTree().ReloadCurrentScene();
	}
}
