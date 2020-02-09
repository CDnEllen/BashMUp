using Godot;
using System;
using System.Collections.Generic;

public class Ship : Node2D
{
	private int speed = 10;

	// Bash vars
	private int bashChargeFrames = 0;
	private int bashMinFrames = 20;
	private Vector2 bashVelocity;
	private float bashVelocityDropoff = 1.05f;
	private float bashMinVelocity = 10;

	// Roll vars
	private bool rollAvailable = true;
	private Vector2 rollDirection = new Vector2();
	private List<bool> rollInputQueue = new List<bool>();
	private int rollInputDuration = 40;
	private int rollInputCountdown;
	private bool rollInputStarted = false;
	private int rollSpeed = 100;

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
			move_input.y-= Input.GetActionStrength("move_forward");
		if (Input.IsActionPressed("move_backward"))
			move_input.y+= Input.GetActionStrength("move_backward");
		if (Input.IsActionPressed("move_left"))
			move_input.x-= Input.GetActionStrength("move_left");
		if (Input.IsActionPressed("move_right"))
			move_input.x+= Input.GetActionStrength("move_right");

		if (move_input.Length() > 1)
			move_input = move_input.Normalized();
		if (bashVelocity != Vector2.Zero)
			move_input.y = 0;
		Position += move_input * speed;

		// Bashing
		if (Input.IsActionPressed("bash_charge_1") && Input.IsActionPressed("bash_charge_2"))
		{
			GD.Print(bashChargeFrames);
			bashChargeFrames++;
		}
		else
		{
			if (bashChargeFrames > bashMinFrames)
				bashVelocity = new Vector2(0.0f, -bashChargeFrames / 2);
			bashChargeFrames = 0;
		}
		Position += bashVelocity;
		bashVelocity /= bashVelocityDropoff;
		if (bashVelocity.Length() < bashMinVelocity)
			bashVelocity = new Vector2();

		// Rolling
		Vector2 inputDir = new Vector2();
		if (Input.IsActionPressed("roll_forward"))
			inputDir.y--;
		if (Input.IsActionPressed("roll_backward"))
			inputDir.y++;
		if (Input.IsActionPressed("roll_left"))
			inputDir.x--;
		if (Input.IsActionPressed("roll_right"))
			inputDir.x++;
		if (inputDir != new Vector2())
			inputDir.Normalized();

		if (inputDir == new Vector2())
			rollAvailable = true;

		if (rollAvailable)
		{
			// Add true/false to the input queue when we stop/start pushing in the roll direction
			if (inputDir != new Vector2())
			{
				if (rollInputQueue.Count == 0 || rollInputQueue.Count == 2)
					rollInputQueue.Add(true);

				if (!rollInputStarted)
				{
					rollDirection = inputDir;
					rollInputStarted = true;
					rollInputCountdown = rollInputDuration;
				}
				else if (rollDirection != inputDir)
				{
					ResetRollVariables();
				}
			}
			else if (inputDir == new Vector2() && rollInputStarted)
			{
				if (rollInputQueue.Count == 1)
					rollInputQueue.Add(false);
			}

			if (rollInputQueue.Count > 3)
				rollInputQueue.RemoveRange(0, rollInputQueue.Count - 3);

			// Perform roll if input requirements are met
			if (rollInputQueue.Count == 3 && rollInputQueue[0] == true && rollInputQueue[1] == false && rollInputQueue[2] == true && rollAvailable == true)
			{
				Position += rollDirection * rollSpeed;
				rollAvailable = false;
				ResetRollVariables();
			}

			// Timer to cancel roll if second tap isn't sent in time
			if (rollInputStarted)
			{
				rollInputCountdown--;
				if (rollInputCountdown <= 0)
				{
					ResetRollVariables();
				}
			}
		}
	}

	private void ResetRollVariables()
	{
		rollInputQueue.Clear();
		rollInputCountdown = rollInputDuration;
		rollInputStarted = false;
		rollDirection = new Vector2();
	}
}
