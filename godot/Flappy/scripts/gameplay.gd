extends Node2D

const ON_RESTART_WAIT_TIME = 3
const MIN_PIPE_POSITION_Y = -150
const MAX_PIPE_POSITION_Y = 150

var _pipe_scene = preload("res://scenes/Pipe.tscn")
var _score : int = 0
var random_number_generator = RandomNumberGenerator.new()

func _ready():
	random_number_generator.randomize()

func _process(_delta):
	self.print_score()
	
func print_score():
	$CanvasLayer/Label.text = "SCORE: %s" % _score

func add_score():
	_score += 1
	
func set_player_died():
	$Player.is_player_died = true
	self.on_restart(ON_RESTART_WAIT_TIME)
	
func on_restart(seconds : float):
	var restart = Timer.new()
	restart.set_wait_time(seconds)
	restart.set_one_shot(true)
	self.add_child(restart)
	restart.start()
	yield(restart, "timeout")
	restart.queue_free()
# warning-ignore:return_value_discarded
	self.get_tree().reload_current_scene()

# receiver method PipeSpawn : Timer timeout()
func _on_pipe_spawn():
	self.pipe_spawn()

func pipe_spawn():
	var new_pipe = _pipe_scene.instance()
	new_pipe.z_index = -10
	new_pipe.position.x = $Position2D.position.x
	new_pipe.position.y = random_number_generator.randf_range(
		MIN_PIPE_POSITION_Y, MAX_PIPE_POSITION_Y)
	add_child(new_pipe)
