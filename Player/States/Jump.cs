using Godot;
using System;
using System.Collections.Generic;

public partial class Jump : PlayerState{
	
	public override void _Process(double delta)	{
		_stateCheck();
	}
	
	private void _stateCheck(){
		if (!_player.IsOnFloor())
			_stateMachine.TransitionTo("Airborn");
		else
			_stateMachine.TransitionTo("Idle");
	}
	
	public override void Enter(Dictionary<string,object> msg = null){
		if(msg == null) msg = new Dictionary<string,object>();
		_player.LoadVelocity();
		_player.velocity.Y = _player._JumpSpeed*-1f;
		_player._JumpFloat = _player._JumpFloatMax;
		_player.SaveVelocity();
	}
}
