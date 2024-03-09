using Godot;
using System;

public partial class Player : CharacterBody2D{
	//OVERHEAD
	[Export] StateMachine _stateMachine;
	[Export] Node2D _body;
	public float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	//ACTION PROPERTIES
	[Export] public float _ForwardWalk = 500f;
	[Export] public float _BackwardWalk = 250f;
	[Export] public float _JumpSpeed = 500f;
	[Export] public float _JumpFloatMax = 900f;
	[Export] public float _FloatDecay = 700f;
	[Export] public float _RollSpeed = 1200f;
	[Export] public float _RollLength = 1000f;
	[Export] public float _LedgeSpeed = 100f;
	[Export] public float _LedgeCoolDownMax = 10f;
	// DYNAMIC ATTRIBUTES	
	public bool FacingLeft = false;
	public float LStickY = 0.0f;
	public float LStickX = 0.0f;
	public float RStickY = 0.0f;
	public float RStickX = 0.0f;
	public bool OnGround = false;
	//
	public float _Height = 0.0f;
	public float _Width = 0.0f;
	public float _Direction = 0.0f;
	public Vector2 velocity = new Vector2();
	public float _JumpFloat = 0.0f;
	public float _LedgeCoolDown = 0.0f;
	//
	public float _root2 = (float)Math.Sqrt(2.0);
	
	public override void _Ready(){
		Rect2 rect = ((Sprite2D)GetNode("Body/Sprite")).GetRect();
		_Width = rect.Size.X*Scale.X;
		_Height = rect.Size.Y*Scale.Y;
		SetPhysicsProcess(false);
	}
	
	public void unhandledInput(InputEvent evnt){
		/* JUST GIVE UP ON THIS SHIT I GUESS
		if (evnt.IsActionPressed("castLeftDown")){
			RStickX = _root2/-2;
			RStickY = _root2/-2;
		} else if (evnt.IsActionPressed("castRightDown")){
			RStickX = _root2/2;
			RStickY = _root2/-2;
		} else if (evnt.IsActionPressed("castRightUP")){
			RStickX = _root2/-2;
			RStickY = _root2/2;
		} else if (evnt.IsActionPressed("castLeftUp")){
			RStickX = _root2/-2;
			RStickY = _root2/2;
		}*/
	}
	
	public override void _PhysicsProcess(double delta){
		_handleSticks();
		velocity = Velocity;
		_handleFall(delta);
		velocity.Y -= _handleFloat((float)delta);
		Velocity = velocity;
		MoveAndSlide();
	}
	
	public override void _Process(double delta){
		_handleSticks();
		_handleLedgeCD((float)delta);
	}
	
	public void FacingDirection() { //For now we'll be mirroring objects
		if (Math.Abs(_Direction) > 0.0f){
			Vector2 scale = _body.Scale;
			scale.X = Math.Sign(_Direction);
			_body.Scale = scale;
			switch (scale.X) {
				case 1:
					FacingLeft = false;
					break;
				case -1:
					FacingLeft = true;
					break;
			}
		}
	}
	
	private void _handleFall(double delta){
		velocity.Y += Gravity * (float)delta;
	}

	private float _handleFloat(float delta) {
		float finalSpeed = _JumpFloat*delta;
		if (_JumpFloat > 0)
			_JumpFloat -= _FloatDecay*delta;
		return finalSpeed;
	}
	
	private void _handleLedgeCD(float delta){
		if (_LedgeCoolDown == 0f) return;
		_LedgeCoolDown -= delta;
		if (_LedgeCoolDown < 0.0f) _LedgeCoolDown = 0.0f;
	}
	
	private void _handleSticks(){
		LStickX = Input.GetAxis("moveLeft", "moveRight");
		_Direction = LStickX;
		LStickY = Input.GetAxis("moveDown", "moveUp");
		RStickX = Input.GetAxis("castLeft", "castRight");
		RStickY = Input.GetAxis("castUp", "castRight");
	}
	/*
	private float handleFloat(float delta) {
		float finalSpeed = _JumpFloat*delta;
		if (_JumpFloat > 0)
			_JumpFloat -= _FloatDecay*delta;
		if (_JumpFloat != 0 && (Input.IsActionJustReleased("jump") || _JumpFloat < 0 || IsOnFloor()))
			_JumpFloat = 0;
		return finalSpeed;
	}*/
	
	public void LoadVelocity() {
		velocity = Velocity;
	}
	
	public void SaveVelocity(){
		Velocity = velocity;
	}
}
