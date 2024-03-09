using Godot;
using System;


public partial class Moving : PlayerState {
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
		_player.LoadVelocity();
		_player.velocity.X = _player._ForwardWalk * _player._Direction;
		_player.SaveVelocity();
	}
	
	public override void _Process(double delta){
		_player.FacingDirection();
		_stateCheck();
	}
	
	private void _stateCheck(){
		if (Input.IsActionJustPressed("jump"))
			_stateMachine.TransitionTo("Jump");
		if (Math.Abs(_player.LStickX) <= 0.0) 
			_stateMachine.TransitionTo("Idle");
		if (Input.IsActionJustPressed("roll"))
			_stateMachine.TransitionTo("Roll");
	}
}
