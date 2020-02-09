using Godot;
using System;

public class Drone : KinematicBody2D
{
	private int counter = 0;
	public bool goLeft = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(float delta)
	{
		Vector2 movement = new Vector2(-2.0f, 0.0f);
		if (goLeft == false)
			movement *= -1;

		if (counter < 60)
		{
			Position += movement;
		}
		else if (counter < 120)
		{
			Position -= movement;
		}

		counter++;
		counter %= 120;
	}
}
