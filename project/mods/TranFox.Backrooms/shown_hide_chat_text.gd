extends Node

# Declare member variables here. Examples:
export var shownText = ""
export var hiddenText = ""
var isIgnored = true

func _on_backrooms_visibility_changed():
	if (isIgnored):
		isIgnored = false
		return
	
	if (get_parent().visible):
		if (shownText != ""):
			Network._update_chat(shownText)
	
	else:
		if (hiddenText != ""):
			Network._update_chat(hiddenText)
