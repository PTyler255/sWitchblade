using Godot;
using System;

public partial class TeliPrototype : CharacterBody2D
{
	[Export] private float _speed = 500.0f;
	//Jump speed handling
	[Export] private float _jumpSpeed = 350.0f;
	[Export] private float _jumpDecay = 1000.0f;
	[Export] private float _jumpFloatMax = 9500.0f;
	private float _jumpFloat;
	

	// Get the gravity from the project settings so you can sync with rigid body nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		velocity.Y += Gravity * (float)delta;

		// Handle jump.
		velocity.Y -= handleJump((float)delta);

		// Get the input direction.
		velocity.X = handleMove((float) delta);

		Velocity = velocity;
		MoveAndSlide();
	}
	
	private float handleMove(float delta){
		handleRoll(delta);
		float direction = Input.GetAxis("moveLeft", "moveRight");
		return direction * _speed;
		
	}
	
	private float handleRoll(float delta) {
		if (!Input.IsActionJustPressed("roll")){
			return 0.0f;
		}
		Sprite2D sprite = (Sprite2D)GetNode("Sprite");
		Color halfOpacity = new Color(1f,1f,1f,0.5f);
		sprite.Modulate = halfOpacity;
		return 0.0f;
	}
	
	private float handleJump(float delta){
		float finalSpeed = _jumpFloat*delta;
		if (_jumpFloat > 0) {
			_jumpFloat -= _jumpDecay*delta;
		}
		if (_jumpFloat != 0 && (Input.IsActionJustReleased("jump") || _jumpFloat < 0 || IsOnFloor()))
			_jumpFloat = 0;
		if (IsOnFloor() && Input.IsActionJustPressed("jump")) {
			finalSpeed = _jumpSpeed;
			_jumpFloat = _jumpFloatMax;
		}
		return finalSpeed;
	}
}
