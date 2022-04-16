extends KinematicBody2D

export var speed = 200
export var xpos_max = 800
var velocity = Vector2()
var direction = 1

func _physics_process(_delta):
	velocity = Vector2()
	
	if position.x < 0:
		direction = 1
	elif position.x > xpos_max:
		direction = -1
	
	velocity.x += direction
	
	print(position.x)
	
	velocity = velocity.normalized() * speed
	velocity = move_and_slide(velocity)
