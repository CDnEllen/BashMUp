using Godot;
using System;

public class Ship : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private int speed = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Vector2 input = new Vector2();
		if (Input.IsActionPressed("move_forward")) 
		{
			input.y--;
		}
		if (Input.IsActionPressed("move_backward"))
		{
			input.y++;
		}
		if (Input.IsActionPressed("move_left"))
		{
			input.x--;
		}
		if (Input.IsActionPressed("move_right"))
		{
			input.x++;
		}
		Position += input.Normalized() * speed;
	}
}
