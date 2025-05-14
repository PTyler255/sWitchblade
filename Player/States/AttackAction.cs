using Godot;
using System;
using System.Collections.Generic;

public partial class AttackAction : PlayerState {
	
	[Export] private AttackHandler _attackHandler;
	private int comboCount = 0;
	
	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
	}
	
	private void GetNextAttack(){
		if (!(_animation.GetQueue()).IsEmpty() && Input.IsActionJustPressed("Attack")){
			if (comboCount > 0) {}
			string currentAttack = _attackHandler._currentAttack;
			string direction = "";
			if (_player.LStickY != 0 && Math.Abs(_player.LStickY) > Math.Abs(_player.LStickX)){
				if(_player.LStickY > 0.0f)
					direction = "Down";
				else direction = "Up";
			}
			GD.Print(direction);
			//_animation.Queue(currentAttack + direction);
		}
	}
	
	public void _stateCheck(string anim){
		if (anim != ""){
			_player.FacingDirection();
			if (!_player.IsOnFloor())
				_stateMachine.TransitionTo("Airborn");
			else
				_stateMachine.TransitionTo("Idle");
		}
	}
	public override void Enter(Dictionary<string,object> msg = null){
		if(msg == null) _stateMachine.TransitionTo("Idle");
		_player.LoadVelocity();
		_player.velocity.X = 0;
		_player.SaveVelocity();
		string nextAttack = (string)msg["direction"];
		nextAttack = _attackHandler._currentAttack + nextAttack;
		/*_animation.Play(nextAttack);
		_animation.Connect("animation_finished", this, "_stateCheck")*/
		GD.Print(nextAttack);
		_stateMachine.TransitionTo("Idle");
	}
	
	public override void Exit(){
		//_animation.disconnect("animation_finished", this, "_stateCheck");
	}
}
