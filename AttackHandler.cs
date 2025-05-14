using Godot;
using System;
using System.Collections.Generic;

public partial class AttackHandler : Node2D{
	
	[Signal] public delegate void AttackSwitchEventHandler(Attack attack);
	[Export] private NodePath _initialAttack = new NodePath();
	private Attack _attack;
	public string _currentAttack;
	private Dictionary<string, Attack> _attackAutomata;
	
	public override void _Ready(){
		_attackAutomata = new Dictionary<string,Attack>();
		_attack = (Attack)GetNode(_initialAttack);
		_currentAttack = _attack.Name;
		_initAutomata();
		//await ToSignal(Owner, "ready");
	}
	
	private void _initAutomata() {
		Godot.Collections.Array<Node> children = GetChildren();
		foreach (Attack child in children)
			_attackAutomata[child.Name] = child;
	}
	
	public void SwitchAttack(String targetAttack, Dictionary<string,object> msg = null){
		if(msg == null) msg = new Dictionary<string,object>();
		GD.Print(_currentAttack + " to " + targetAttack);
		if (!_attackAutomata.ContainsKey(targetAttack)){
			GD.Print("There's no attack:" + targetAttack);
			return ;
		}
		_attack.Exit();
		_attack = _attackAutomata[targetAttack];
		_currentAttack = targetAttack;
		EmitSignal(SignalName.AttackSwitch, _attack);
		_attack.Enter(msg);
	}
	
	/*
	public string GetAttackDirection(Player player){
		string direction = "";
		int facingSign = -1;
		if (player.FacingLeft) facingSign = 1;
		if (facingSign*player._Direction < 0) direction = "Back";
		if (player.LStickY != 0 && Math.Abs(_player.LStickY) > Math.Abs(player.LStickX)){
			if(player.LStickY > 0.0f)
				direction = "Up";
			else direction = "Down";
		}
		return direction;
	}*/
}
