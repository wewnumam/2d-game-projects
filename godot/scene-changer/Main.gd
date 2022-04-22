extends Control


func _on_ButtonTo2D_pressed():
	get_tree().change_scene("res://Scene2D.tscn")

func _on_ButtonTo3D_pressed():
	get_tree().change_scene("res://Scene3D.tscn")

func _on_ButtonQuit_pressed():
	get_tree().quit()

func _on_ButtonToUI_pressed():
	get_tree().change_scene("res://Control.tscn")
