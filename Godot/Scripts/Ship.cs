using Godot;
using System;
using System.Collections.Generic;

public class Ship : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private int speed = 10;

	private bool readyToRoll = true;
	private List<bool> rollInputQueue = new List<bool>();
	private int rollInputDuration = 100;
	private int rollInputCountdown;
	private bool rollInputStarted = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// Movement
		Vector2 move_input = new Vector2();
		if (Input.IsActionPressed("move_forward")) 
			move_input.y-= Input.GetActionStrength("move_forward"); ;
		if (Input.IsActionPressed("move_backward"))
			move_input.y+= Input.GetActionStrength("move_backward");
		if (Input.IsActionPressed("move_left"))
			move_input.x-= Input.GetActionStrength("move_left");
		if (Input.IsActionPressed("move_right"))
			move_input.x+= Input.GetActionStrength("move_right");

		if (move_input.Length() > 1)
			move_input = move_input.Normalized();
		Position += move_input * speed;

		// Rolling
		if (Input.IsActionJustPressed("roll_left")) 
		{
			rollInputQueue.Add(true);
			if (rollInputStarted == false)
			{
				rollInputStarted = true;
				rollInputCountdown = rollInputDuration;
			}
		}
		else if (Input.IsActionJustReleased("roll_left") && rollInputStarted)
		{
			rollInputQueue.Add(false);
		}

		if (rollInputQueue.Count == 3 && rollInputQueue[0] == true && rollInputQueue[1] == false && rollInputQueue[2] == true)
		{
			Position += new Vector2(-100.0f, 0.0f);
			rollInputStarted = false;
			rollInputCountdown = rollInputDuration;
			rollInputQueue.Clear();
		}

		if (rollInputStarted)
		{
			rollInputCountdown--;
			if (rollInputCountdown <= 0)
			{
				rollInputStarted = false;
				rollInputCountdown = rollInputDuration;
				rollInputQueue.Clear();
			}
		}

		if (rollInputQueue.Count > 3)
		{
			rollInputQueue.RemoveRange(0, rollInputQueue.Count - 3);
		}

		//var result = "blah " + string.Join(", ", rollInputQueue.ToArray());
		//GD.Print(result);
	}
}
