using Godot;
using System;

public partial class Health : Node {
	[Export] private int _maxHealth = 30;
	[Export] private float _maxPoise = 5f;
	[Export] private float _poiseRegen = 1f;
	[Export] private float _maxPoiseDelay = 2f;
	[Export] private CharacterBody2D character;
	private int _currentHealth;
	private float _currentPoise;
	private int _tempHealth = 0;
	private float _poiseDelay = 0.0f;
	
	[Signal] public delegate void HealthDepletedEventHandler();
	[Signal] public delegate void PoiseDepletedEventHandler();
	[Signal] public delegate void PoiseRefillEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		_currentHealth = _maxHealth;
		_currentPoise = _maxPoise;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta){
		PoiseRegen((float)delta);
	}
	
	private void PoiseRegen(float delta) {
		if (_poiseDelay > 0.0){
			_poiseDelay -= _poiseRegen*delta;
			return;
		}
		if (_currentPoise >= _maxPoise) return;
		_currentPoise += _poiseRegen*delta;
		if (_currentPoise < _maxPoise) return;
		_currentPoise = _maxPoise;
		EmitSignal(SignalName.PoiseRefill);
	}
	
	public void PoiseDamage(float damage){
		_poiseDelay = _maxPoiseDelay;
		if (_currentPoise < 0.0) return;
		_currentPoise -= damage;
		if (_currentPoise > 0.0) return;
		EmitSignal(SignalName.PoiseRefill);
	}
	
	public void Damage(int damage) {
		_tempHealth -= damage;
		if (_tempHealth >= 0) return ;
		_currentHealth += _tempHealth;
		_tempHealth = 0;
		if (_currentHealth > 0) return;
		EmitSignal(SignalName.HealthDepleted);
	}
}




