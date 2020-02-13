using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public class Level : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Dictionary data;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Node saveLoad = (Node)GetTree().GetNodesInGroup("SaveLoad")[0];
		data = (Dictionary)saveLoad.GetScript().Call("load_level_file");

		GD.Print(data);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
