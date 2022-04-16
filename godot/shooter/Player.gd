extends KinematicBody2D

onready var bullet_scene = preload("res://Bullet.tscn")
export var speed = 250
export var bullet_speed = 1000
var velocity = Vector2()

func _physics_process(_delta):
	look_at(get_global_mouse_position())
	velocity = Vector2()
	
	if Input.is_action_pressed("ui_left"):
		velocity.x -= 1
	elif Input.is_action_pressed("ui_right"):
		velocity.x += 1
	if Input.is_action_pressed("ui_up"):
		velocity.y -= 1
	elif Input.is_action_pressed("ui_down"):
		velocity.y += 1

	velocity = velocity.normalized() * speed
	velocity = move_and_slide(velocity)
	
	if Input.is_action_just_pressed("fire"):
		var bullet =  bullet_scene.instance()
		get_node("/root/Main").add_child(bullet)
		bullet.position = position
		bullet.rotation_degrees = rotation_degrees
		bullet.apply_impulse(Vector2(), Vector2(bullet_speed, 0).rotated(rotation))
