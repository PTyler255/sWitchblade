using Godot;
using System;
using System.Collections.Generic;

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
		else if (Math.Abs(_player.LStickX) <= 0.0) 
			_stateMachine.TransitionTo("Idle");
		else if (Input.IsActionJustPressed("attack")){
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
		else if (Input.IsActionJustPressed("roll"))
			_stateMachine.TransitionTo("Roll");
	}
}
