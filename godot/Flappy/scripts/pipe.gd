extends StaticBody2D

const SPEED = 100

var player : Node2D = null

func _ready():
	player = self.get_parent().get_node("Player");

func _process(delta):
	if not player.is_player_died:
		self.move_pipe(delta)
		
	if self.is_pipe_out_frame():
		self.queue_free()
	
func move_pipe(delta):
	self.position.x += -SPEED * delta
	
func is_pipe_out_frame():
	return self.position.x < -50

# receiver method Area2D : Area2D body_entered()
func _on_player_passing_pipe(body):
	if body.name == "Player":
		self.get_node("/root/Gameplay").add_score()
