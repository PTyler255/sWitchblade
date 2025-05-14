using Godot;
using System;
using System.Collections.Generic;

public partial class Airborn : PlayerState {
	
	[Export] private float _lerpSpeed = 4f;
	[Export] LedgeDetection  _ledgeHitBox;
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
		//if (!(Math.Abs(_player._Direction) > 0.0f)) return;
		_player.LoadVelocity();
		float finalSpeed = _player._ForwardWalk * _player._Direction;
		if (Math.Abs(_player.velocity.X) > _player._ForwardWalk && _player.velocity.X*_player._Direction > 0.0)
			finalSpeed = _player.velocity.X;
		_player.velocity.X = finalSpeed;
		_player.velocity = _player.Velocity.Lerp(_player.velocity, (float)delta*_lerpSpeed);
		_player.SaveVelocity();
	}
	
	public override void _Process(double delta)	{
		if (_player._JumpFloat != 0 && (Input.IsActionJustReleased("jump") || _player._JumpFloat < 0 )) 
			_player._JumpFloat = 0;
		_stateCheck();
	}
	
	private void _stateCheck(){
		if (_player.IsOnFloor()){
			/*if (!(Math.Abs(_player._Direction) > 0.0f))
				_stateMachine.TransitionTo("Idle");
			else*/ _stateMachine.TransitionTo("Moving");
		}
		else if (_player.Velocity.Y > _player._LedgeSpeed && _ledgeHitBox.GetCanGrab() && !(_player._LedgeCoolDown > 0.0f)) {
			Dictionary<string, object> msg = new Dictionary<string, object>();
			msg["position"] = _ledgeHitBox.GetGrabLocation();
			_stateMachine.TransitionTo("Hang", msg);
		}
	}
	public override void Exit(){
		_player._JumpFloat = 0;
	}
}
