using Godot;
using System;
using System.Collections.Generic;

public partial class Hang : PlayerState {
	
	private Vector2 zero = new Vector2();
	private Vector2 position = new Vector2();
	private Vector2 grabPosition = new Vector2();
	[Export] private float _lerpSpeed = 10f;
	
	public override void _PhysicsProcess(double delta){
		_player.velocity = zero;
		_player.SaveVelocity();
		_player.Position = _player.Position.Lerp(position, (float)delta*_lerpSpeed);
	}
	
	public override void _Process(double delta){
		_stateCheck();
	}
	
	private void _stateCheck(){
		if (Input.IsActionJustPressed("jump"))
			_stateMachine.TransitionTo("Jump");
		if (_player.LStickY < -0.5f)
			_stateMachine.TransitionTo("Airborn");
		if (_player.LStickY > 0.5f){
			position.X = grabPosition.X;
			position.Y = grabPosition.Y - _player._Height/8f;
			_player.Position = position;
			_stateMachine.TransitionTo("Idle");
		}
	}
	
	public override void Enter(Dictionary<string,object> msg){
		if(msg == null) _stateMachine.TransitionTo("Airborn");
		grabPosition = (Vector2)msg["position"];
		position = _player.Position;
		float wallDirection = grabPosition.X - position.X;
		_player._Direction = Math.Sign(wallDirection);
		_player.FacingDirection();
		position.X = grabPosition.X + _player._Width/8f*_player._Direction*-1;
		position.Y = grabPosition.Y + _player._Height/8f;
		_player.velocity = zero;
		_player.SaveVelocity();
		//_player.Position = position;
	}
	public override void Exit(){
		position = zero;
		_player._LedgeCoolDown = _player._LedgeCoolDownMax;
	}
}
