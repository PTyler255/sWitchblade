using Godot;
using System;

public partial class Health : Node
{
	[Export] private int _maxHealth = 30;
	[Export] private int _maxPoise = 5;
	[Export] private float _poiseRegen = 1f;
	private int _currentHealth;
	private int _currentPoise;
	private int _tempHealth = 0;
	private Timer _poiseTimer;
	public bool _Stunned = false;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		_currentHealth = _maxHealth;
		_currentPoise = _maxPoise;
		_poiseTimer = (Timer)GetNode("Poise");
		_poiseTimer.WaitTime = _poiseRegen;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		handlePoise((float)delta);
	}
	
	private void handlePoise(float delta) {
		if (_poiseTimer.IsStopped() && _currentPoise < _maxPoise) {
			_poiseTimer.Start();
		}
		if (_currentPoise <= 0) {
			_Stunned = true;
		} else {
			_Stunned = false;
		}
	}
	
	private void Damage(int damage) {
		_tempHealth -= damage;
		if (_tempHealth >= 0) {
			return ;
		}
		_currentHealth += _tempHealth;
		_tempHealth = 0;
		//TODO: hitstun
	}

	private void _on_poise_timeout(){
		_currentPoise += 1;
	}
}




