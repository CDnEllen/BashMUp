using Godot;
using System;

public class EnemySpawner : Node2D
{
	private bool spawnEnemies = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		if (spawnEnemies)
		{
			var gen = new RandomNumberGenerator();
			gen.Randomize();

			for (int i = 0; i < 5; i++)
			{
				var enemyScene = (PackedScene)ResourceLoader.Load("res://Scenes/Drone.tscn");
				Drone instance = (Drone)enemyScene.Instance();
				instance.Position = new Vector2(gen.RandfRange(-500f, 500f), gen.RandfRange(-300f, 100f));
				instance.goLeft = gen.Randf() >= 0.5f;
				AddChild(instance);
			}
			spawnEnemies = false;
		}
	}
}
