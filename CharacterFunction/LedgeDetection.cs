using Godot;
using System;

public partial class LedgeDetection : Node2D{
	
	private CharacterBody2D _parent;
	private Node2D _wallPoint;
	private Node2D _ledgePoint;
	private RayCast2D _wallRayCastLeft;
	private RayCast2D _wallRayCastRight;
	private RayCast2D _ledgeRayCast;
	private int _itemsInArea = 0;
	[Export] public RayCast2D _groundRayCast;
	private bool _canGrab;
	public Vector2 _grabLocation;
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
		_canGrab = false;
		_grabLocation = new Vector2();
		_wallPoint.Position = Vector2.Zero;
		_ledgePoint.Position = Vector2.Zero;
		if (_groundRayCast.IsColliding()) return;
		if (_wallRayCastLeft.IsColliding()){
			_wallPoint.GlobalPosition = _wallRayCastLeft.GetCollisionPoint();
			if (!_ledgeRayCast.IsColliding()) return;
			_ledgePoint.GlobalPosition = _ledgeRayCast.GetCollisionPoint();
		} else if (_wallRayCastRight.IsColliding()){
			_wallPoint.GlobalPosition = _wallRayCastRight.GetCollisionPoint();
			if (!_ledgeRayCast.IsColliding()) return;
			_ledgePoint.GlobalPosition = _ledgeRayCast.GetCollisionPoint();
		} else return;
		if (_itemsInArea != 0 || !_ledgeRayCast.IsColliding()) return;
		_canGrab = true;
		_grabLocation = _ledgePoint.GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta){
		handleLedgeDetection();
	}
	
	public override void _Process(double delta){
		if (_itemsInArea < 0)
			_itemsInArea = 0;
	}
	
	public bool GetCanGrab(){
		return _canGrab;
	}
	
	public Vector2 GetGrabLocation(){
		return _grabLocation;
	}
	
	private void _on_area_2d_area_entered(Area2D area){
		_itemsInArea += 1;
	}
	private void _on_area_2d_area_exited(Area2D area){
		_itemsInArea -= 1;
	}
	private void _on_area_2d_body_entered(Node2D body) {
		_itemsInArea += 1;
	}
	private void _on_area_2d_body_exited(Node2D body) {
		_itemsInArea -= 1;
	}
	
}











