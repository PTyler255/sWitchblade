using Godot;
using System;
using System.Collections.Generic;

//Long Sword = standard
//Greatsword = Damage + Knockback
//Mace = Poise
//Rapier = Range/Sweetspot


public partial class Attack : Node2D{
	
	[Export] public float baseDamage = 5;
	[Export] public float basePoise = 1;
	[Export] public Vector2 baseKnockback = new Vector2();
	[Export] public float sweetSpotMod = 1.0f;
	[Export] public CollisionPolygon2D sweetSpot;
	[Export] public int damageType = 0;
	
	enum Damage {
		BASE,
		FIRE
	}
	
	[Signal] public delegate void AttacksEventHandler(float damage, float poise, Vector2 knockback);
	
	public float GetDamage(){
		return baseDamage;
	}
	public int GetDamageType(){
		return damageType;
	}
	public float GetPoise(){
		return basePoise;
	}
	public Vector2 GetKnocback(){
		return baseKnockback;
	}
	public virtual void Enter(Dictionary<string,object> msg = null){
		if(msg == null) msg = new Dictionary<string,object>();
	}
	public virtual void Exit(){}
}
