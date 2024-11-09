extends Node

const BACKROOMS_AREA = preload("res://mods/TranFox.Backrooms/Backrooms_Area.tscn")
var isDone = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# Hacky way to do this... I'm new to gdscript alright...
	if (!isDone):
		var zonesNode = get_node_or_null("/root/world/Viewport/main/map/main_map/zones/");
		
		if (zonesNode != null):
			var new_instance = BACKROOMS_AREA.instance()
			add_child(new_instance)
			var new_node = get_node("backrooms")
			new_node.set_visible(false)
			new_node.global_translate(Vector3(300, 5, 300))
			remove_child(new_node)
			zonesNode.add_child(new_node)
			new_node.set_owner(zonesNode)
			isDone = true
			print("Added backrooms zone.")
			print(self.get_path())

# Called after getting stuck
func MaybeGoToBackroooms():
	var rand = randf()
	if (rand < (20.0/100.0)):
		print("To the backrooms with you.")
		
		# Shouldn't happen but id like to be safe
		var player = get_node_or_null("/root/world/Viewport/main/entities/player")
		if (player == null):
				return
		
		player.world._enter_zone("backrooms", - 1)
		player.last_valid_pos = get_node("/root/world/Viewport/main/map/main_map/zones/backrooms/entry").global_transform.origin

#func _input(ev):
#	if Input.is_key_pressed(KEY_EQUAL):
#		var player = get_node_or_null("/root/world/Viewport/main/entities/player")
#		if (player == null):
#				return
#
#		player._kill()
#		MaybeGoToBackroooms()
