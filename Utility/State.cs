using Godot;
using System;
using System.Collections.Generic;

public partial class State : Node {
	
	protected StateMachine _stateMachine;
	protected Node _parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		SetProcess(false);
		SetPhysicsProcess(false);
		_stateMachine = (StateMachine)Owner.GetNode("StateMachine");
		_parent = (Node)GetParent();
	}

	public virtual void Enter(Dictionary<string,object> msg = null){
		if(msg == null) msg = new Dictionary<string,object>();
	}
	public virtual void Exit(){}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){}
	public override void _PhysicsProcess(double delta){}
	public virtual void unhandledInput(InputEvent evnt){}
}
