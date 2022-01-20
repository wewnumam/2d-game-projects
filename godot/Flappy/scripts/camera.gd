extends Node2D

var player : Node2D = null

func _ready():
	player = get_parent().get_node("Player")

func _process(_delta):
	self.position = player.position
