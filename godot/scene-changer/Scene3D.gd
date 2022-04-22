extends Spatial

func _on_ButtonBack_pressed():
	get_tree().change_scene("res://Main.tscn")

func _on_StaticBody_mouse_entered():
	get_tree().change_scene("res://Main.tscn")
