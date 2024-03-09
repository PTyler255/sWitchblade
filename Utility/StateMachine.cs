using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node{
	
	[Signal] public delegate void transitionedEventHandler(State state);
	[Export] private NodePath _initialState = new NodePath();
	private State _state;
	public string _currentState;
	private Dictionary<string, State> _stateAutomata;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		_stateAutomata = new Dictionary<string,State>();
		_state = (State)GetNode(_initialState);
		_currentState = _state.Name;
		_initAutomata();
		//await ToSignal(Owner, "ready");
		_state.Enter();
	}
	
	public void _print(){
		GD.Print("TEST");
	}

	private void _initAutomata() { _initAutomataRecursion(GetChildren());} 
	private void _initAutomataRecursion(Godot.Collections.Array<Node> children) {
		foreach (State child in children){
			_stateAutomata[child.Name] = child;
			if (child.GetChildCount() == 0) continue;
			_initAutomataRecursion(child.GetChildren());
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		_state._Process(delta);
	}
	public override void _PhysicsProcess(double delta){
		_state._PhysicsProcess(delta);
	}
	public override void _UnhandledInput(InputEvent evnt){
		_state.unhandledInput(evnt);
	}
	public void TransitionTo(String targetState, Dictionary<string,object> msg = null){
		if(msg == null) msg = new Dictionary<string,object>();
		GD.Print(_currentState + " to " + targetState);
		if (!_stateAutomata.ContainsKey(targetState)){
			GD.Print("There's no state:" + targetState);
			return ;
		}
		_state.Exit();
		_state = _stateAutomata[targetState];
		_currentState = targetState;
		_state.Enter(msg);
	}
}
