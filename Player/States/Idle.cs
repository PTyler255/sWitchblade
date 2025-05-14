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
		else if (Input.IsActionJustPressed("jump"))
			_stateMachine.TransitionTo("Jump");
		else if (Math.Abs(_player.LStickX) > 0.0) 
			_stateMachine.TransitionTo("Moving");
		if (Input.IsActionJustPressed("attack")){
			string direction = "";
			if (_player.LStickY != 0 && Math.Abs(_player.LStickY) > Math.Abs(_player.LStickX)){
				if(_player.LStickY > 0.0f)
					direction = "Down";
				else direction = "Up";
			}
			Dictionary<string,object> msg = new Dictionary<string,object>();
			msg["direction"] = direction;
			_stateMachine.TransitionTo("AttackAction", msg);
		}
		if (Input.IsActionJustPressed("roll"))
			_stateMachine.TransitionTo("Roll");
	}
	
	public override void unhandledInput(InputEvent evnt){
		_player.unhandledInput(evnt);
	}
	
	public override void Enter(Dictionary<string,object> msg = null){

	}

}
