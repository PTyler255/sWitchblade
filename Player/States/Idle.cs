using Godot;
using System;
using System.Collections.Generic;

public partial class Idle : PlayerState {

	[Export] public float _lerpSpeed = .1f;

	public override void _Process(double delta)	{
		_player.FacingDirection();
		_stateCheck();
	}
	
	private void _stateCheck(){
		if (!_player.IsOnFloor())
			_stateMachine.TransitionTo("Airborn");
		if (Input.IsActionJustPressed("jump"))
			_stateMachine.TransitionTo("Jump");
		if (Math.Abs(_player.LStickX) > 0.0) 
			_stateMachine.TransitionTo("Moving");
		if (Input.IsActionJustPressed("roll"))
			_stateMachine.TransitionTo("Roll");
	}
	
	public override void unhandledInput(InputEvent evnt){
		_player.unhandledInput(evnt);
	}
	
	public override void Enter(Dictionary<string,object> msg = null){

	}

}
