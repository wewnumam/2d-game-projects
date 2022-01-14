using Godot;
using System;

public class MainCamera : Node2D
{
    private Node2D player;
    private bool isPlayerDied;
    
    public override void _Ready()
    {
        player = GetParent().GetNode<Node2D>("Player");
    }

    public override void _Process(float delta)
    {
        if (isPlayerDied) 
            return;
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector2 playerHorizontalPosition = new Vector2(
            x: player.Position.x, y: Position.y);
        Position = playerHorizontalPosition;
    }

    public void SetPlayerDied(bool isPlayerDied)
    {
        this.isPlayerDied = isPlayerDied;
    }
}
