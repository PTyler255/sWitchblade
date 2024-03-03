using Godot;
using System;

public partial class LedgeGrab : Node2D{
	
	private CharacterBody2D _parent;
	private Node2D _wallPoint;
	private Node2D _ledgePoint;
	private RayCast2D _wallRayCastLeft;
	private RayCast2D _wallRayCastRight;
	private RayCast2D _ledgeRayCast;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		_parent = (CharacterBody2D)GetNode("../");
		_wallRayCastLeft = (RayCast2D)GetNode("WallRayCastLeft");
		_wallRayCastRight = (RayCast2D)GetNode("WallRayCastRight");
		_wallPoint = (Node2D)GetNode("WallPoint");
		_ledgeRayCast = (RayCast2D)GetNode("WallPoint/LedgeRayCast");
		_ledgePoint = (Node2D)GetNode("WallPoint/LedgeRayCast/LedgePoint");
	}

	private void handleLedgeDetection(){
		if (_wallRayCastLeft.IsColliding()){
			_wallPoint.GlobalPosition = _wallRayCastLeft.GetCollisionPoint();
			_ledgePoint.GlobalPosition = _ledgeRayCast.GetCollisionPoint();
		} else if (_wallRayCastRight.IsColliding()){
			_wallPoint.GlobalPosition = _wallRayCastRight.GetCollisionPoint();
			_ledgePoint.GlobalPosition = _ledgeRayCast.GetCollisionPoint();
		} else {
			_wallPoint.Position = Vector2.Zero;
			_ledgePoint.Position = Vector2.Zero;
		}
	}
	
	private void handleLedgeClimb(){} {
		//TODO
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		handleLedgeDetection();
		handleLedgeClimb();
	}
}
