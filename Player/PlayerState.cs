using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerState : State {
	
	protected Player _player;
	protected AnimationPlayer _animation;
	public Dictionary<string, Dictionary<string, object>> _nextState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		_player = (Player)Owner;
		_animation = (AnimationPlayer)_player.GetNode("AnimationPlayer");
		base._Ready();
	}
	public override void _PhysicsProcess(double delta){ _player._StatePhysicsProcess(delta);}
}
