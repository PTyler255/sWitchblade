using Godot;
using System;

public partial class Hitbox : Node{
	
	[Export] public Area2D Hurtbox;
	
	[Signal] public delegate void PoiseDamageEventHandler(float poiseDamage);
	[Signal] public delegate void DamageEventHandler(int damage, int damageType);
	[Signal] public delegate void KnockbackEventHandler(Vector2 knockback);
	
	private void handleDamage(int damage, int damageType = 0, float poiseDamage = 0.0f ){
		EmitSignal(SignalName.PoiseDamage, poiseDamage);
		EmitSignal(SignalName.Damage, damage, damageType);
	}
	private void handleKnockback(Vector2 knockback){
		EmitSignal(SignalName.Knockback, knockback);
	}
	private void _on_area_2d_area_entered(Area2D area) {
		/*
		Attack attack = area as Attack;
		if (attack == null) return;
		handleDamage(attack.GetDamage(), attack.GetDamageType(), attack.GetPoise());
		handleKnockback(attack.GetKnockback());
	}
	private void _on_area_2d_body_entered(Node2D body){
		var attack = body as Attack;
		if (attack == null) return;
		handleDamage(attack.GetDamage(), attack.GetDamageType(), attack.GetPoise());
		handleKnockback(attack.GetKnockback());*/
	}
}





