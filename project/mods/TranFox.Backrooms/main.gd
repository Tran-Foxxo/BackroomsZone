extends Node

const BACKROOMS_AREA = preload("res://mods/TranFox.Backrooms/Backrooms_Area.tscn")
var wasAdded = false

func _addBackroomsZone():
	print("Adding backrooms zone...")
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
		print("Added backrooms zone.")
		wasAdded = true
	else:
		print("Couldn't find zones node...")
		print("BACKROOMS ZONE WAS NOT ADDED")
		wasAdded = false		

# Called after getting stuck
func _maybeGoToBackroooms():
	if (!wasAdded):
		print("Tried to go the backrooms when it doesn't seem to exist...")
		return
	
	var rand = randf()
	if (rand < (20.0/100.0)):
		print("To the backrooms with you.")
		
		# Shouldn't happen but id like to be safe
		var player = get_node_or_null("/root/world/Viewport/main/entities/player")
		if (player == null):
				return
		
		var backroomsEntryNode = get_node_or_null("/root/world/Viewport/main/map/main_map/zones/backrooms/entry")
		if (backroomsEntryNode != null):
			player.world._enter_zone("backrooms", - 1)
			player.last_valid_pos = backroomsEntryNode.global_transform.origin
		else:
			print("Couldn't find the backrooms zone node.")
