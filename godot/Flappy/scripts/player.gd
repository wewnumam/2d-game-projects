extends KinematicBody2D

const FLAP_FORCE = 150
const PLAYER_MASS = 100

var _player : Vector2 = Vector2()
var is_player_died : bool = false

onready var gravity = ProjectSettings.get_setting(
	"physics/2d/default_gravity")

func _physics_process(delta):
	self.on_gravity(delta)
	
	if not is_player_died and Input.is_action_just_pressed("flap"):
		self.on_flap()
		
	_player = self.move_and_slide_with_snap(
		_player, Vector2.DOWN, Vector2.UP)

func on_flap():
	_player.y = -FLAP_FORCE

func on_gravity(delta):
	_player.y += (gravity + PLAYER_MASS) * delta

# receiver method Area2D : Area2D body_entered()
func _on_player_body_entered(body):
	if body.is_in_group("pipe") or body.name == "Ground":
		self.get_node("/root/Gameplay").set_player_died()
