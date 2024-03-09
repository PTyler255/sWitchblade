using Godot;
using System;
using System.Collections.Generic;

public partial class Roll : PlayerState {
	
	[Export] public float _RollSpeed = 1200f;
	[Export] public float _RollDecay = 1200f;
	[Export] public float _RollThreshold = 600f;
	private float _Roll = 0;
	private float _direction = 1f;
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
		_player.LoadVelocity();
		_player.velocity.X = _Roll;
		_Roll += _RollDecay*(float)delta*(_direction*-1);
		_player.SaveVelocity();
	}
	
	public override void _Process(double delta){
		_stateCheck();
	}
	
	private void _stateCheck(){
		if (Math.Abs(_Roll) < _RollThreshold)
			_stateMachine.TransitionTo("Moving");
		if (Input.IsActionJustPressed("jump"))
			_stateMachine.TransitionTo("Jump");
		if (!_player.IsOnFloor())
			_stateMachine.TransitionTo("Airborn");
	}
	
	public override void Enter(Dictionary<string,object> msg = null){
		if(msg == null) msg = new Dictionary<string,object>();
		_direction = Math.Sign(_player._Direction);
		_Roll = _RollSpeed*_direction;
	}
	
	public override void Exit(){
		_Roll = 0.0f;
		_direction = 0;
		_player.LoadVelocity();
		/*_player.velocity.X = _player._ForwardWalk*_player._Direction;
		_player.SaveVelocity();*/
	}
}
