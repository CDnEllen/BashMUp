extends Node


# Declare member variables here. Examples:
# var a: int = 2
# var b: String = "text"



export var levels = {}

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	
	
	pass # Replace with function body.

static func load_level_file() -> Dictionary:
	var yaml = preload("res://addons/godot-yaml/gdyaml.gdns").new()
	var file = File.new()
	file.open("res://Data/levels.yaml", file.READ)
	var text = file.get_as_text()
	var value = yaml.parse(text)
	
	return value

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta: float) -> void:
#	pass
