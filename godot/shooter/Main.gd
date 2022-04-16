extends Node2D

onready var player = $KinematicBody2D
onready var camera = $Camera2D
onready var mouse = $Position2D

func _physics_process(_delta):
	mouse.position = get_local_mouse_position()
	camera.position = player.position

func _on_Area2D_body_entered(body):
	pass # Replace with function body.
